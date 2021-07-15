using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Player : MonoBehaviour
{
    private const string SPAWN_POINT = "Spawn Point";


    [Header("Attributes")]
    [SerializeField] public bool hasControl = true;
    [SerializeField] public int shardsCollected = 0;
    [SerializeField] public bool canShoot = false;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float shotPower = 1f;
    

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject deathTransitionPrefab;
    private Rigidbody2D rb2d;
    private Rigidbody2D groundRigidbody;


    [Header("Sounds")]
    [SerializeField] private AudioClip jumpAudioClip;
    [Range(1, 100)] [SerializeField] private int jumpAudioVolume = 100;


    [Header("Movement")]
    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private float speed = 8f;
    private Vector2 velocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;
    private Vector2 intentDirection;
    public bool grounded = true;
    [SerializeField] private float groundCheckDistance = 0.1f;
    private bool isJumping = false;
    [SerializeField] private float jumpHoldTime = 0.3f;
    [SerializeField] private float gravityIncreaseRate = 1;
    private float jumpTimeCounter = 0;

    [Header("Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private float cameraYTweakDelta = 0.2f;
    [SerializeField] private float cameraTweakDelay = 0.5f;
    private float cameraTweakHoldTime = 0;
    private float defaultCameraY = 0;

    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    
    private static Player _instance;
    public static Player Instance
    {
        get {
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.UpdateUI();
        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        defaultCameraY = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;

        Respawn();
        
        SceneManager.sceneLoaded += OnLoadCallback;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isDialogUp)
        {
            intentDirection = Vector2.zero;
        }
        

        animator.SetBool("isGrounded", grounded);
    }

    void FixedUpdate()
    {
        velocity += Physics2D.gravity * Time.deltaTime;
        Move();
        CalculateJumpHeight();
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject spawn = GameObject.Find(SPAWN_POINT);
        if (spawn)
        {
            transform.position = GameObject.Find(SPAWN_POINT).transform.position;
            groundRigidbody = GameObject.Find("Ground").GetComponent<Rigidbody2D>();
        }

        virtualCamera = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        defaultCameraY = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Danger"))
        {
            Die();
        }

        if (collision.gameObject.tag == "Collectible")
        {
            Collectible collectible = collision.gameObject.GetComponent<Collectible>();
            if (collectible.type == Collectible.ItemType.Shard)
            {
                shardsCollected++;
                GameManager.Instance.UpdateUI();
            }
            if (collectible.type == Collectible.ItemType.Inventory)
            {
                AddInventoryItem(collision.gameObject.name, collectible.inventorySprite);
            }
        }
    }

    private void Respawn()
    {
        // Place at set spawn
        transform.position = GameObject.Find(SPAWN_POINT).transform.position;
        groundRigidbody = GameObject.Find("Ground").GetComponent<Rigidbody2D>();
    }

    private void Move()
    {
        targetVelocity.y = Mathf.Min(targetVelocity.y, 0);
        targetVelocity.x = intentDirection.x * speed;
        animator.SetFloat("speed", Mathf.Abs(targetVelocity.x));

        if (intentDirection.x < -0.01)
        {
            spriteRenderer.flipX = true;
        }
        else if (intentDirection.x > 0.01)
        {
            spriteRenderer.flipX = false;
        }

        RaycastHit2D[] hits = new RaycastHit2D[16];
        int hitCount = rb2d.Cast(targetVelocity, contactFilter, hits, groundCheckDistance);
        bool againstWall = false;
        for (int i = 0; i < hitCount; i++)
        {
            if (hits[i].collider.gameObject.layer != 8) // Layer 8 is enemy
            {
                againstWall = true;
            }
        }

        velocity.x = againstWall ? 0 : targetVelocity.x;

        // Check if camera has to move
        if (intentDirection.y == 1)
        {
            if (cameraTweakHoldTime >= cameraTweakDelay)
            {
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = defaultCameraY + cameraYTweakDelta;
            }
            else
            {
                cameraTweakHoldTime += Time.deltaTime;
            }
        }
        else if (intentDirection.y == -1)
        {
            if (cameraTweakHoldTime >= cameraTweakDelay)
            {
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = defaultCameraY - cameraYTweakDelta;
            }
            else
            {
                cameraTweakHoldTime += Time.deltaTime;
            }
        }
        else
        {
            if (cameraTweakHoldTime >= cameraTweakDelay)
            {
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = defaultCameraY;
            }
            cameraTweakHoldTime = 0;
        }
    }

    private void InitialJump()
    {
        isJumping = true;
        jumpTimeCounter = jumpHoldTime;
        AudioPlayer.Instance.PlaySFX(jumpAudioClip, jumpAudioVolume);
        grounded = false;
        animator.SetBool("isGrounded", false);
    }

    private void CalculateJumpHeight()
    {
        bool groundCheck = false;
        RaycastHit2D[] hits = new RaycastHit2D[16];
        

        hits = new RaycastHit2D[16];
        int hitCount;
        if (velocity.y > 0)
        {
            hitCount = rb2d.Cast(Vector2.up, contactFilter, hits, groundCheckDistance);
            for (int i = 0; i < hitCount; i++)
            {
                isJumping = false;
                velocity.y = 0;
            }
        }
        else if (velocity.y <= 0 && !isJumping)
        {
            hitCount = rb2d.Cast(Vector2.down, contactFilter, hits, groundCheckDistance);
            for (int i = 0; i < hitCount; i++)
            {
                groundCheck = true;
            }
            grounded = groundCheck;
        }
        


        if (isJumping && jumpTimeCounter > 0)
        {
            velocity.y = jumpPower;            
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;

            if (grounded)
            {
                // If grounded, don't push down so hard. This should be enough to keep feet on ground
                velocity.y = -0.5f;
            }
            else
            {
                velocity += gravityIncreaseRate * Physics2D.gravity * Time.deltaTime;
            }
        }

        animator.SetFloat("verticalVelocity", velocity.y);
        rb2d.velocity = velocity;
    }

    public void AddInventoryItem(string inventoryName, Sprite image)
    {
        inventory.Add(inventoryName, image);
        GameManager.Instance.inventorySprite.sprite = image;
    }

    public void RemoveInventoryItem(string inventoryName)
    {
        if (inventory.ContainsKey(inventoryName))
        {
            inventory.Remove(inventoryName);
            GameManager.Instance.inventorySprite.sprite = GameManager.Instance.inventoryEmptySprite;
        }
    }

    public void Bounce(float bouncePower)
    {
        grounded = false;
        animator.SetBool("isGrounded", false);
        velocity.y = bouncePower;
    }

    // Currently used for moving platforms to override position
    public void VelocityTweak(Vector3 additionalVelocity)
    {
        float transX = 0;
        float transY = 0;
        if (velocity.x == 0)
        {
            transX = additionalVelocity.x;
        }

        if (grounded)
        {
            Vector2 move = new Vector2(transX, transY);
            RaycastHit2D[] hits = new RaycastHit2D[16];
            int hitCount = rb2d.Cast(move, hits, groundCheckDistance);
            bool againstWall = false;
            for (int i = 0; i < hitCount; i++)
            {
                if (hits[i].collider.gameObject.tag == "Surface")
                {
                    againstWall = true;
                }
            }
            
            if (!againstWall)
            {
                transform.Translate(move);
            }
        }
        // else
        // {
        //     transX = additionalVelocity.x + velocity.x;
        //     transY = additionalVelocity.z + velocity.y;
        //     rb2d.velocity = new Vector2(transX, transY);
        // }
    }

    public void Die()
    {
        StartCoroutine(DeathSequence());
    }

    public IEnumerator DeathSequence()
    {
        // Reset all forces
        velocity.y = 0;
        targetVelocity = Vector2.zero;

        spriteRenderer.enabled = false;
        Player.Instance.hasControl = false;
        rb2d.simulated = false;
        GameObject deathTransition = Instantiate(deathTransitionPrefab);
        GameObject slider = GameObject.Find("Ripple");
        Animator animator = slider.GetComponent<Animator>();
        animator.SetTrigger("start");
        yield return new WaitForSeconds(0.6f);
        spriteRenderer.enabled = true;
        Player.Instance.hasControl = true;
        Respawn();
        rb2d.simulated = true;
        animator.SetTrigger("end");
        yield return new WaitForSeconds(0.6f);
        Destroy(deathTransition);
    }

    // Player Input System
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (hasControl)
            {
                intentDirection = context.ReadValue<Vector2>();
            }
        }

        if (context.canceled)
        {
            intentDirection = Vector2.zero;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (grounded && hasControl)
            {
                InitialJump();
            }
        }

        if (context.canceled)
        {
            isJumping = false;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed && hasControl && canShoot)
        {
            Vector2 shotDirection = intentDirection.normalized;

            if (intentDirection.x == 0 && intentDirection.y == 0)
            {
                if (!spriteRenderer.flipX)
                {
                    shotDirection = Vector2.right.normalized;
                }
                else
                {
                    shotDirection = Vector2.left.normalized;
                }
            }

            Vector2 spawnLocation = transform.position;
            spawnLocation = spawnLocation + shotDirection.normalized;

            GameObject shootObject = Instantiate(shootPrefab, spawnLocation, Quaternion.identity);
            Rigidbody2D shotRb = shootObject.GetComponent<Rigidbody2D>();
            Physics2D.IgnoreCollision(shootObject.GetComponent<CapsuleCollider2D>(), GetComponent<BoxCollider2D>());

            if (shotRb)
            {
                animator.SetTrigger("shoot");
                shotRb.AddForce(shotDirection / 10 * shotPower, ForceMode2D.Force);
            }
        }
    }
}

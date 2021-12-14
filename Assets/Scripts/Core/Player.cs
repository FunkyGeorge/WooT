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
    public bool isDying = false;
    

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
    [SerializeField] private AudioClip crunchAudioClip;
    [Range(1, 100)] [SerializeField] private int crunchAudioVolume = 100;


    [Header("Movement")]
    [SerializeField] private ContactFilter2D contactFilter;
    [SerializeField] private float speed = 8f;
    private Vector2 velocity = Vector2.zero;
    private Vector2 targetVelocity = Vector2.zero;
    [SerializeField] private float minMoveDeadzone = 0.2f;
    private Vector2 momentum = Vector2.zero;
    private Vector2 intentDirection;
    public bool grounded = true;
    private float lastGrounded = 0;
    [SerializeField] float momentumClingTime = 0.75f;
    private bool hardGrounded = false; // Used if the player should stick to it's current surface
    [SerializeField] private float groundCheckDistance = 0.1f;

    [Header("Jumping")]
    [SerializeField] private float jumpHoldTime = 0.3f;
    private bool isJumping = false;
    [SerializeField] private float gravityIncreaseRate = 1;
    private float jumpTimeCounter = 0;
    [SerializeField] private float fastFallInitialSpeed = 2.5f;
    [SerializeField] private float fastFallDownControlThreshold = 0.5f;
    private float lastJumpTime = 0;
    private float lastBounceTime = 0;
    [SerializeField] private float extraBounceTimeThreshold = 0.5f;
    [SerializeField] private float extraBounceBonus = 10f;

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
        CheckSurroundings();
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

        GameObject vcGameObject = GameObject.Find("Virtual Camera");

        if (vcGameObject)
        {
            virtualCamera = vcGameObject.GetComponent<CinemachineVirtualCamera>();
            defaultCameraY = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY;
        }
    }

    public void PrepareForDestroy()
    {
        SceneManager.sceneLoaded -= OnLoadCallback;
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

    private void CheckSurroundings()
    {
        // TODO: move againstWall calculation to here
        float contactCheckDistance = 0.005f;

        RaycastHit2D[] hits = new RaycastHit2D[16];
        RaycastHit2D[] hits2 = new RaycastHit2D[16];
        int leftHits = rb2d.Cast(Vector2.left, contactFilter, hits, contactCheckDistance);
        int rightHits = rb2d.Cast(Vector2.right, contactFilter, hits2, contactCheckDistance);
        
        if (leftHits > 0 && rightHits > 0)
        {
            List<string> uniqueHorizontalColliders = new List<string>(10);
            for (int i = 0; i < leftHits; i++)
            {
                if (!uniqueHorizontalColliders.Contains(hits[i].collider.gameObject.name))
                {
                    uniqueHorizontalColliders.Add(hits[i].collider.gameObject.name);
                }
            }

            for (int i = 0; i < rightHits; i++)
            {
                if (!uniqueHorizontalColliders.Contains(hits2[i].collider.gameObject.name))
                {
                    uniqueHorizontalColliders.Add(hits2[i].collider.gameObject.name);
                }
            }

            if (uniqueHorizontalColliders.Count > 1)
            {
                if (!isDying)
                {
                    AudioPlayer.Instance.PlaySFX(crunchAudioClip, crunchAudioVolume);
                    Die();
                }
            }
        }

        hits = new RaycastHit2D[16];
        hits2 = new RaycastHit2D[16];
        int topHits = rb2d.Cast(Vector2.up, contactFilter, hits, contactCheckDistance);
        int bottomHits = rb2d.Cast(Vector2.down, contactFilter, hits2, contactCheckDistance);

        if (topHits > 0 && bottomHits > 0)
        {
            List<string> uniqueVerticalColliders = new List<string>(10);
            for (int i = 0; i < topHits; i++)
            {
                if (!uniqueVerticalColliders.Contains(hits[i].collider.gameObject.name))
                {
                    uniqueVerticalColliders.Add(hits[i].collider.gameObject.name);
                }
            }

            for (int i = 0; i < bottomHits; i++)
            {
                if (!uniqueVerticalColliders.Contains(hits2[i].collider.gameObject.name))
                {
                    uniqueVerticalColliders.Add(hits2[i].collider.gameObject.name);
                }
            }

            if (uniqueVerticalColliders.Count > 1)
            {
                // Only kill player if different colliders, shouldn't be squashed by a single collider
                if (!isDying)
                {
                    AudioPlayer.Instance.PlaySFX(crunchAudioClip, crunchAudioVolume);
                    Die();
                }
            }
        }
    }

    private void Move()
    {
        targetVelocity.y = Mathf.Min(targetVelocity.y, 0);
        targetVelocity.x = Mathf.Abs(intentDirection.x) > minMoveDeadzone ? intentDirection.x * speed : 0;
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
        if (intentDirection.y > 0.95f)
        {
            animator.SetBool("isLookingUp", true);
            if (cameraTweakHoldTime >= cameraTweakDelay)
            {
                virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = defaultCameraY + cameraYTweakDelta;
            }
            else
            {
                cameraTweakHoldTime += Time.deltaTime;
            }
        }
        else if (intentDirection.y < -0.95f)
        {
            animator.SetBool("isCrouching", true);
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
            animator.SetBool("isLookingUp", false);
            animator.SetBool("isCrouching", false);
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
        animator.SetBool("isLookingUp", false);
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
                if (hits[i].collider.gameObject.name == "Ground")
                {
                    momentum = Vector2.zero;
                }
            }
            grounded = groundCheck;
        }
        


        if (isJumping && jumpTimeCounter > 0)
        {
            velocity.y = jumpPower;            
            jumpTimeCounter -= Time.deltaTime;
            if (momentum != Vector2.zero)
            {
                velocity += momentum;
            }
        }
        else
        {
            isJumping = false;

            if (grounded)
            {
                // If grounded, don't push down so hard. This should be enough to keep feet on ground
                lastGrounded = Time.time;
                velocity.y = hardGrounded ? -4f : -0.5f;
                hardGrounded = false;
            }
            else
            {
                if (momentum != Vector2.zero)
                {
                    velocity += momentum;
                }
                
                if (intentDirection.y < -fastFallDownControlThreshold)
                {
                    velocity += new Vector2(0, -fastFallInitialSpeed);
                }
                else
                {
                    velocity += gravityIncreaseRate * Physics2D.gravity * Time.deltaTime;
                }
            }
        }

        if (lastGrounded + momentumClingTime < Time.time)
        {
            // Dump momemntum
            momentum = Vector2.zero;
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

    private void Shoot()
    {
        Vector2 shotDirection = intentDirection;
        shotDirection.x = Mathf.Round(shotDirection.x);
        shotDirection.y = Mathf.Round(shotDirection.y);
        shotDirection = shotDirection.normalized;

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

    public void Bounce(float bouncePower)
    {
        grounded = false;
        animator.SetBool("isGrounded", false);
        velocity.y = bouncePower;

        if (Time.time - lastJumpTime <= extraBounceTimeThreshold)
        {
            velocity.y += extraBounceBonus;
        }

        lastBounceTime = Time.time;
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

        momentum = new Vector2(additionalVelocity.x/Time.deltaTime, additionalVelocity.z/Time.deltaTime);

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

            if (additionalVelocity.y < 0)
            {
                // Smash player to the ground
                hardGrounded = true;
            }
            
            if (!againstWall)
            {
                transform.Translate(move);
            }
        }
    }

    public void Die()
    {
        if (!isDying)
        {
            isDying = true;
            StartCoroutine(DeathSequence());
        }
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
        Animator slideAnimator = slider.GetComponent<Animator>();
        slideAnimator.SetTrigger("start");
        yield return new WaitForSeconds(0.6f);
        spriteRenderer.enabled = true;
        Player.Instance.hasControl = true;
        Respawn();
        rb2d.simulated = true;
        slideAnimator.SetTrigger("end");
        yield return new WaitForSeconds(0.6f);
        Destroy(deathTransition);
        isDying = false;
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
            
            lastJumpTime = Time.time;
            if (Time.time - lastBounceTime <= extraBounceTimeThreshold)
            {
                velocity.y += extraBounceBonus;
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
            Shoot();
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (GameManager.Instance.isPaused)
            {
                GameManager.Instance.UnpauseGame();
            }
            else
            {
                GameManager.Instance.PauseGame();
            }
        }
    }
}

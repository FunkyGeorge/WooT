using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : PhysicsObject
{
    private const string SPAWN_POINT = "Spawn Point";


    [Header("Attributes")]
    [SerializeField] public bool hasControl = true;
    [SerializeField] public int shardsCollected = 0;
    [SerializeField] public bool canShoot = false;
    [SerializeField] private float jumpPower = 7f;
    [SerializeField] private float shotPower = 1f;
    [SerializeField] private float speed = 8f;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject shootPrefab;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Sounds")]
    [SerializeField] private AudioClip jumpAudioClip;
    [SerializeField] private AudioClip landAudioClip;
    [Range(1, 100)] [SerializeField] private int jumpAudioVolume = 100;
    [Range(1, 100)] [SerializeField] private int landAudioVolume = 100;


    public Dictionary<string, Sprite> inventory = new Dictionary<string, Sprite>();
    private Vector2 intentDirection;
    private bool isJumping = false;
    [SerializeField] private float jumpHoldTime = 0.3f;
    private float jumpTimeCounter = 0;
    

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

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.UpdateUI();

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
        Move();

        CalculateJumpHeight();

        animator.SetBool("isGrounded", grounded);
        animator.SetFloat("verticalVelocity", velocity.y);
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode)
    {
        GameObject spawn = GameObject.Find(SPAWN_POINT);
        if (spawn)
        {
            transform.position = GameObject.Find(SPAWN_POINT).transform.position;
            groundRigidbody = GameObject.Find("Ground").GetComponent<Rigidbody2D>();
        }
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
    }

    private void CalculateJumpHeight()
    {
        if (isJumping && jumpTimeCounter > 0)
        {
            velocity.y = jumpPower;
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
        }
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
        animator.SetBool("isGrounded", false);
        velocity.y = bouncePower;
    }

    public void Die()
    {
        // Reset all forces
        velocity.y = 0;
        targetVelocity = Vector2.zero;

        Respawn();
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
                isJumping = true;
                jumpTimeCounter = jumpHoldTime;
                AudioPlayer.Instance.PlaySFX(jumpAudioClip, jumpAudioVolume);
                animator.SetBool("isGrounded", false);
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

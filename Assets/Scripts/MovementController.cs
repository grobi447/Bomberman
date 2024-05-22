using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class MovementController : MonoBehaviour
{
    private MovementControllerModel model = new MovementControllerModel();
    public Rigidbody2D rb { get; private set; }
    public Vector2 moveDirection = Vector2.down;
    public float moveSpeed = 5.0f;
    public bool speedboosted = false;
    public KeyCode upKey = KeyCode.W;
    public KeyCode downKey = KeyCode.S;
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    public AnimatedSpriteRenderer spriteRendererDeath;

    private AnimatedSpriteRenderer spriteRendererIdle;

    AudioManager audioManager;
    private bool isRunning = false;
    public bool ghost;
    public bool invincible;

    //Az Awake a szkript betöltésekor kerül meghívásra
    //A Rigidbody2D komponens inicializálása
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRendererIdle = spriteRendererDown;

    }
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void OnEnable()
    {
        ghost = false;
        invincible = false;
    }


    //A FixedUpdate a fizikai frissítésekhez használt metódus
    //A karakter mozgatása a megfelelő billentyű lenyomásával (Folymatos mozgás)
    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(position + translation);
    }

    //Update metódus, amely a játék minden egyes képkockánál lefut
    //Karakter mozgatása a megfelelő billentyű lenyomásával
    void Update()
    {

        if (Input.GetKey(upKey))
        {

            setMoveDirection(Vector2.up, spriteRendererUp);
        }
        else if (Input.GetKey(downKey))
        {
            setMoveDirection(Vector2.down, spriteRendererDown);
        }
        else if (Input.GetKey(leftKey))
        {
            setMoveDirection(Vector2.left, spriteRendererLeft);
        }
        else if (Input.GetKey(rightKey))
        {
            setMoveDirection(Vector2.right, spriteRendererRight);
        }
        else
        {
            setMoveDirection(Vector2.zero, spriteRendererIdle);
        }
        if (moveDirection != Vector2.zero)
        {
            if (!isRunning)
            {
                audioManager.PlayLoopingSFX(audioManager.run);
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                audioManager.StopLoopingCurrentSFX();
                isRunning = false;
            }
        }
    }


    //setMoveDirection metódus, amely beállítja a mozgás irányát
    public void setMoveDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        moveDirection = newDirection;

        model.moveDirection = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        spriteRendererIdle = spriteRenderer;
        spriteRendererIdle.idle = moveDirection == Vector2.zero;
    }

    //Sebesség növeléséhez használt segédfüggvény
    public void IncreaseSpeed()
    {
        if (speedboosted == false)
        {
            moveSpeed++;
            speedboosted = true;
        }
    }

    //Sebesség csökkentéséhez használt segédfüggvény
    public async void LowSpeed()
    {
        float current;
        if (speedboosted == false)
        {
            current = 5.0f;
        }
        else
        {
            current = 6.0f;
        }
        moveSpeed = 3.0f;
        await Task.Delay(5000);
        moveSpeed = current;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion") && invincible == false)
        {
            DeathAction();
        }
        if (other.gameObject.CompareTag("Brick") && !ghost)
        {
            rb.velocity = Vector2.zero;
            other.isTrigger = false;
        }
        if (other.gameObject.CompareTag("Brick") && ghost)
        {
            GetComponent<BombController>().isOnBomb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Circle") && invincible == false)
        {
            DeathAction();
        }
        if (other.gameObject.CompareTag("Brick"))
        {
            other.isTrigger = true;
            GetComponent<BombController>().isOnBomb = false;
        }
    }

    //Halál közbeni állapot kezelése
    private void DeathAction()
    {
        if (isRunning)
        {
            audioManager.StopLoopingCurrentSFX();
            isRunning = false;
        }
        audioManager.PlaySFX(audioManager.death);
        GetComponent<Collider2D>().enabled = false;
        enabled = false;
        GetComponent<BombController>().enabled = false;

        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        spriteRendererLeft.enabled = false;
        spriteRendererRight.enabled = false;
        spriteRendererDeath.enabled = true;

        Invoke(nameof(DeathActionEnded), 1.25f);
    }

    private void DeathActionEnded()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().CheckGameEnd();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ghost") && invincible == false)
        {
            DeathAction();
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Player") || (collision.gameObject.layer == LayerMask.NameToLayer("Bomb") && ghost == true) || (collision.gameObject.CompareTag("Brick") && ghost == true))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }

    }
    public async void Ghost()
    {
        ghost = true;
        await Task.Delay(4000);
        ghost = false;
    }

    public async void Invincible()
    {
        invincible = true;
        await Task.Delay(6000);
        invincible = false;
    }

}

public class MovementControllerModel
{
    public Vector2 moveDirection { get; set; }
    public float moveSpeed { get; private set; }
    public bool speedboosted { get; set; }
    public bool invincible { get; set; }
    public bool ghost { get; set; }

    public void IncreaseSpeed()
    {
        if (speedboosted == false)
        {
            moveSpeed++;
            speedboosted = true;
        }
    }

    public async void LowSpeed()
    {
        float current = speedboosted ? 6.0f : 5.0f;
        moveSpeed = 3.0f;
        await Task.Delay(5000);
        moveSpeed = current;
    }

    public async void Invincible()
    {
        invincible = true;
        await Task.Delay(6000);
        invincible = false;
    }

    public async void Ghost()
    {
        ghost = true;
        await Task.Delay(4000);
        ghost = false;
    }
}

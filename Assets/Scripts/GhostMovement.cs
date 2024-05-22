using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }
    private Vector2 moveDirection = Vector2.zero;
    public float moveSpeed = 5.0f;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    private AnimatedSpriteRenderer spriteRendererIdle;
    AudioManager audioManager;
    private bool canjump = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        chooseDirection();
    }
    private void FixedUpdate()
    {
        Vector2 position = rb.position;
        Vector2 translation = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(position + translation);


        float modX = rb.position.x % 1;
        float modY = rb.position.y % 1;
        if ((modX < 0.1 || modX > 0.9) && (modY < 0.1 || modY > 0.9))
        {

            chooseDirection();
        }
    }
    private void setMoveDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        jumpToCenter();
        moveDirection = newDirection;

        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        spriteRendererIdle = spriteRenderer;
        spriteRendererIdle.idle = moveDirection == Vector2.zero;


    }
    private void chooseOppsiteDirection()
    {

        jumpToCenter();
        if (moveDirection == Vector2.up)
        {
            setMoveDirection(Vector2.down, spriteRendererDown);
        }
        else if (moveDirection == Vector2.down)
        {
            setMoveDirection(Vector2.up, spriteRendererUp);
        }
        else if (moveDirection == Vector2.left)
        {
            setMoveDirection(Vector2.right, spriteRendererRight);
        }
        else if (moveDirection == Vector2.right)
        {
            setMoveDirection(Vector2.left, spriteRendererLeft);
        }
    }

    private void chooseDirection()
    {
        if (!canjump) return;

        jumpToCenter();


        List<Vector2> directions = new List<Vector2> { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

        for (int i = directions.Count - 1; i >= 0; i--)
        {
            Collider2D collider = Physics2D.OverlapCircle(rb.position + directions[i], 0.1f);
            if (collider != null && (collider.tag == "Wall" || collider.tag == "Brick"))
            {
                directions.RemoveAt(i);
            }
        }

        Vector2 newDirection;
        if (directions.Count > 1)
        {
            directions.Remove(-moveDirection);
        }
        if (directions.Count > 0)
        {
            int randomIndex = UnityEngine.Random.Range(0, directions.Count);
            newDirection = directions[randomIndex];
        }
        else
        {
            return;
        }

        if (newDirection == Vector2.up)
        {
            setMoveDirection(Vector2.up, spriteRendererUp);
        }
        else if (newDirection == Vector2.down)
        {
            setMoveDirection(Vector2.down, spriteRendererDown);
        }
        else if (newDirection == Vector2.left)
        {
            setMoveDirection(Vector2.left, spriteRendererLeft);
        }
        else if (newDirection == Vector2.right)
        {
            setMoveDirection(Vector2.right, spriteRendererRight);
        }

        moveDirection = newDirection;

        StartCoroutine(DeactivateChooseDirection());
    }

    private IEnumerator DeactivateChooseDirection()
    {
        canjump = false;
        yield return new WaitForSeconds(0.33f);
        canjump = true;
    }
    private void jumpToCenter()
    {
        Vector2 position = rb.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);
        rb.MovePosition(position);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            chooseOppsiteDirection();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            chooseOppsiteDirection();
        }
        if (collision.gameObject.CompareTag("Brick"))
        {
            chooseOppsiteDirection();
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(collision.collider, GetComponent<Collider2D>());
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            audioManager.PlaySFX(audioManager.ghostDeath);
            Destroy(gameObject);
        }
    }
}
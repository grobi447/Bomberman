using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Bomb : MonoBehaviour
{
    public Rigidbody2D rb { get; private set; }

    public Explosion explosionPrefab;
    public float explosionDuration = 1.0f;
    public LayerMask explosionLayer;
    public Tilemap ExplodeTiles;
    public Explode explode;
    public BombController PlacedBy { get; set; }
    public float bombCooldown = 3.0f;
    AudioManager audioManager;
    private bool exploded = false;

    private void Awake()
    {

        ExplodeTiles = GameObject.FindGameObjectWithTag("Brick").GetComponent<Tilemap>();

        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    void Start()
    {
        PlacedBy.bombsRemaining--;
        if (PlacedBy.detonator == false)
        {
            StartCoroutine(PlaceBomb());

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            exploded = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
          
            audioManager.StopBombTimer();
            audioManager.PlaySFX(audioManager.bombExplosion);
  
            StartCoroutine(Explode(position, Vector2.up, PlacedBy.explosionRadius));
            StartCoroutine(Explode(position, Vector2.down, PlacedBy.explosionRadius));
            StartCoroutine(Explode(position, Vector2.left, PlacedBy.explosionRadius));
            StartCoroutine(Explode(position, Vector2.right, PlacedBy.explosionRadius));

        }
    }
    public void ClearTile(Vector2 position)
    {
        Vector3Int cell = ExplodeTiles.WorldToCell(position);
        TileBase tile = ExplodeTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(explode, position, Quaternion.identity);
            ExplodeTiles.SetTile(cell, null);
        }
    }

    private IEnumerator Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            Destroy(gameObject);
            yield break;
        }
        position += direction;
        yield return new WaitForSeconds((float)0.25); //terjedés sebesége
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayer))
        {
            ClearTile(position);
            yield break;
        }
        
        audioManager.PlaySFX(audioManager.bombExplosion);
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end);
        explosion.SetDirection(direction);
        Destroy(explosion.gameObject, explosionDuration);

        StartCoroutine(Explode(position, direction, length - 1));
    }

    public IEnumerator PlaceBomb()
    {
        if (!exploded)
        {
            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);
            if (PlacedBy.detonator == false)
            {
                audioManager.PlayBombTimer();
                yield return new WaitForSeconds(bombCooldown);
            }
            gameObject.GetComponent<Collider2D>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            PlacedBy.bombsRemaining++;
            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            explosion.SetActiveRenderer(explosion.start);
            audioManager.PlaySFX(audioManager.bombExplosion);
            Destroy(explosion.gameObject, explosionDuration);
            ClearTile(position);
            StartCoroutine(Explode(position, Vector2.up, PlacedBy.explosionRadius));
            StartCoroutine(Explode(position, Vector2.down, PlacedBy.explosionRadius));
            StartCoroutine(Explode(position, Vector2.left, PlacedBy.explosionRadius));
            StartCoroutine(Explode(position, Vector2.right, PlacedBy.explosionRadius));
        }
    }
}

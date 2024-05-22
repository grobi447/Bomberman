using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class BombController : MonoBehaviour
{
    public GameObject bombPrefab;
    public KeyCode dropBombKey = KeyCode.Space;
    public int maxBombs = 1;
    public int bombsRemaining = 1;
    public int explosionRadius = 1;
    public bool dontplacebomb = false;
    public bool placebomb = false;
    public bool isOnBomb = false;
    public bool detonator;
    public AnimatedSpriteRenderer spriteRenderer;
    private List<Bomb> bombs = new List<Bomb>();

    //OnEnable metódus, amely a szkript aktíválásakor kerül meghívásra
    private void OnEnable()
    {
        bombsRemaining = maxBombs;
        detonator = false;
    }

    //Update metódus, amely a játék minden egyes képkockánál lefut
    void Update()
    {
        if ((Input.GetKeyDown(dropBombKey) && bombsRemaining > 0 && dontplacebomb == false || placebomb == true && bombsRemaining > 0 && dontplacebomb == false) && !isOnBomb && detonator == false)
        {
            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            GameObject bombObject = Instantiate(bombPrefab, position, Quaternion.identity);
            Bomb bomb = bombObject.GetComponent<Bomb>();
            bomb.PlacedBy = this;
        }
        else if (Input.GetKeyDown(dropBombKey) && bombsRemaining > 0 && detonator == true && !isOnBomb)
        {
            Vector2 position = transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            GameObject bombObject = Instantiate(bombPrefab, position, Quaternion.identity);

            Bomb bomb = bombObject.GetComponent<Bomb>();
            bomb.PlacedBy = this;
            bombs.Add(bomb);
        }
        else if (Input.GetKeyDown(dropBombKey) && bombsRemaining == 0 && detonator == true && !isOnBomb)
        {
            foreach (Bomb bomb in bombs)
            {
                bomb.StartCoroutine(bomb.PlaceBomb());
            }
            bombs.Clear();
            detonator = false;
        }
    }
    //A bomba elhagyása után nem lehet átmenni rajta
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
            isOnBomb = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            isOnBomb = true;
        }
    }

    public void AddBomb()
    {
        bombsRemaining++;
        maxBombs++;
    }

    public void IncreaseRadius()
    {
        explosionRadius++;
    }

    public async void LowRange()
    {
        int current = explosionRadius;
        explosionRadius = 1;
        await Task.Delay(10000);
        explosionRadius = current;
    }

    public async void NoBomb()
    {
        dontplacebomb = true;
        await Task.Delay(5000);
        dontplacebomb = false;
    }

    public async void PlaceAllBombs()
    {
        placebomb = true;
        await Task.Delay(5000);
        placebomb = false;
    }

    public void setBombKey()
    {
        dropBombKey = GetComponent<PlayerControllLoad>().GetBomb();
    }

}

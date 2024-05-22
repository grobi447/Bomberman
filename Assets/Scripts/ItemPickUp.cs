using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    AudioManager audioManager;
    public enum ItemType
    {
        ExtraBomb,
        ExtraBombRange,
        PlusSpeed,
        Detonator,
        Invincible,
        Ghost,
        Barricade,
        LowSpeed,
        LowBombRange,
        NoBomb,
        PlaceAllBombs,
    }

    public ItemType type;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    //Power up felvétele megtörténik
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnItemPickUp(other.gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            Destroy(gameObject);
        }
    }

    //Power up felvételével kapcsolatos játékos tulajdonságok kezelése
    private void OnItemPickUp(GameObject player)
    {
        audioManager.PlaySFX(audioManager.powerUp);
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb();
                break;
            case ItemType.ExtraBombRange:
                player.GetComponent<BombController>().IncreaseRadius();
                break;
            case ItemType.PlusSpeed:               
                player.GetComponent<MovementController>().IncreaseSpeed();            
                break;
            case ItemType.Detonator:
                player.GetComponent<BombController>().detonator = true;
                break;
            case ItemType.Invincible:               
                player.GetComponent<MovementController>().Invincible();
                break;
            case ItemType.Ghost:               
                player.GetComponent<MovementController>().Ghost();
                break;
            case ItemType.Barricade:               
                
                break;
            case ItemType.LowSpeed:               
                player.GetComponent<MovementController>().LowSpeed();
                break;
            case ItemType.LowBombRange:               
                player.GetComponent<BombController>().LowRange();
                break;
            case ItemType.NoBomb:               
                player.GetComponent<BombController>().NoBomb();
                break;
            case ItemType.PlaceAllBombs:               
                player.GetComponent<BombController>().PlaceAllBombs();
                break;
        }

        Destroy(gameObject);
    }
    
}

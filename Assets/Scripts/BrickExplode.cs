using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float explosionDuration = 1.0f;

    [Range(0f,1f)]
    public float spawnChance = 0.3f;
    public GameObject[] spawnableItems;

    //A robbanás prefabot a robbanás időtartama után törli
    void Start()
    {
        Destroy(gameObject, explosionDuration);    
    }

    //Doboz felrobbanása utáni item dobás esélyét és megtörténését kezeli
    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && UnityEngine.Random.value < spawnChance)
        {
            int randomIndex = UnityEngine.Random.Range(0, spawnableItems.Length);
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity);
        }
    }
}

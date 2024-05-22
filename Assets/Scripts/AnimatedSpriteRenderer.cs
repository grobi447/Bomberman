using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float animationTime = 0.25f;
    private int animationFrame = 0;
    public Sprite[] animationSprites;
    public Sprite idleSprite;
    public bool loop = true;
    public bool idle = true;
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;

    //Az Awake a szkript betöltésekor kerül meghívásra
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Az OnEnable a szkript aktíválásakor kerül meghívásra
    //A spriteRenderer beállítása láthatóvá
    private void OnEnable()
    {
        if (spriteRenderer == null)
        {
            return;
        }
        spriteRenderer.enabled = true;
    }

    //Az OnDisable a szkript deaktiválásakor kerül meghívásra
    //A spriteRenderer beállítása láthatatlanná
    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }

    //Az animáció kezdőképkockájának beállítása
    void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime);
    }

    //Ha a képkocka a végére ér, és a loop be van kapcsolva, akkor a következő képkocka az első lesz
    private void NextFrame()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length && loop)
        {
            animationFrame = 0;
        }
        if (idle)
        {
            spriteRenderer.sprite = idleSprite;
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length)
        {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
}

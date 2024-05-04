using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clipMasking : MonoBehaviour
{
    void Awake()
    {
        // Disables the sprite renderer of all children
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.enabled = false;
        }
    }
}
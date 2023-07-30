using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerColorChanger : MonoBehaviour
{
    public Color[] colors;
    private int currentColorIndex = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = colors[currentColorIndex];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ColorChanger"))
        {
            currentColorIndex++;
            if (currentColorIndex >= colors.Length)
            {
                currentColorIndex = 0;
            }

            spriteRenderer.color = colors[currentColorIndex];

            Destroy(collision.gameObject);
        }
    }

    public void UpdateColor()
    {
        int randomColorIndex = Random.Range(0, colors.Length);
        spriteRenderer.color = colors[randomColorIndex];
    }
}


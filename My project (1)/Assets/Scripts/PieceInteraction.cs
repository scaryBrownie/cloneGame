using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceInteraction : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            SpriteRenderer playerSpriteRenderer = other.GetComponent<SpriteRenderer>();

            if (spriteRenderer.color != playerSpriteRenderer.color)
            {
                other.gameObject.GetComponent<PlayerController>().DeathPlayer();
                transform.parent.gameObject.SetActive(false);
            }
        }
    }
}

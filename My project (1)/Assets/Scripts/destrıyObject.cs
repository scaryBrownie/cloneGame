using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destrıyObject : MonoBehaviour
{
    private void FixedUpdate()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            float playerPosY = playerObject.transform.position.y;

            if (transform.position.y + 40 < playerPosY)
            {
                Destroy(gameObject);
            }
        }
    }
}

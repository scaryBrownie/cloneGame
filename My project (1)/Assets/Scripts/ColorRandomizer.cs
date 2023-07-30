using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRandomizer : MonoBehaviour
{
    public bool useRandomColors = false;

    private PlayerColorChanger playerColorChanger;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerColorChanger = playerObject.GetComponent<PlayerColorChanger>();
        }

        Color[] childColors;

        if (useRandomColors)
        {
            childColors = GetRandomColors();
        }
        else
        {
            childColors = GetOrderedColors();
        }

        UpdateChildColors(childColors);
    }

    private Color[] GetRandomColors()
    {
        if (playerColorChanger != null)
        {
            Color[] playerColors = playerColorChanger.colors;
            int childCount = transform.childCount;

            Color[] randomColors = new Color[childCount];

            ShuffleArray(playerColors);

            for (int i = 0; i < childCount; i++)
            {
                Color randomColor = playerColors[i % playerColors.Length];
                randomColors[i] = randomColor;
            }

            return randomColors;
        }

        return null;
    }

    private Color[] GetOrderedColors()
    {
        if (playerColorChanger != null)
        {
            Color[] playerColors = playerColorChanger.colors;
            int childCount = transform.childCount;

            Color[] orderedColors = new Color[childCount];

            for (int i = 0; i < childCount; i++)
            {
                Color orderedColor = playerColors[i % playerColors.Length];
                orderedColors[i] = orderedColor;
            }

            return orderedColors;
        }

        return null;
    }

    private void UpdateChildColors(Color[] childColors)
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);
            SpriteRenderer childRenderer = child.GetComponent<SpriteRenderer>();

            childRenderer.color = childColors[i];
        }
    }

    private void ShuffleArray<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorAnimation : MonoBehaviour
{
    public float colorChangeSpeed = 1f;
    public Direction direction;

    private SpriteRenderer[] squares;
    private Color[] colors;
    private int currentIndex = 0;
    private float colorChangeTimer = 0f;

    public enum Direction
    {
        Left,
        Right
    }

    private void Start()
    {
        squares = GetComponentsInChildren<SpriteRenderer>();
        colors = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerColorChanger>().colors;

        SetSquareColors();
    }

    private void Update()
    {
        if (currentIndex >= colors.Length)
        {
            currentIndex = 0;
        }

        colorChangeTimer += Time.deltaTime;
        if (colorChangeTimer >= colorChangeSpeed)
        {
            colorChangeTimer = 0f;
            ChangeSquareColors();
        }
    }

    private void ChangeSquareColors()
    {
        Color firstColor = squares[0].color;

        for (int i = 0; i < squares.Length - 1; i++)
        {
            squares[i].color = squares[i + 1].color;
        }

        squares[squares.Length - 1].color = firstColor;

        currentIndex++;
    }

    private void SetSquareColors()
    {
        for (int i = 0; i < squares.Length; i++)
        {
            squares[i].color = colors[i];
        }
    }
}

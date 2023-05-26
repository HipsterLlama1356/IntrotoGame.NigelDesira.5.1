using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    private Camera mainCamera;
    private string[] hexColorCodes;
    private int currentIndex = 0;
    private float colorChangeInterval = 3f; // Time interval in seconds
    private float transitionDuration = 1f; // Duration of the color transition
    private float timer;
    private Color initialColor;
    private Color targetColor;
    private float transitionTimer;
    private bool isTransitioning;

    private void Start()
    {
        mainCamera = Camera.main;
        hexColorCodes = new string[]
        {
            "#C6807A",
            "#D4695F",
            "#D44033",
            "#8E3831",
            "#7B2923"
        };

        timer = colorChangeInterval;
        initialColor = mainCamera.backgroundColor;
        targetColor = HexToColor(hexColorCodes[currentIndex]);
        mainCamera.backgroundColor = initialColor;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            StartColorTransition();
            timer = colorChangeInterval;
        }

        if (isTransitioning)
        {
            UpdateColorTransition();
        }
    }

    private void StartColorTransition()
    {
        currentIndex = (currentIndex + 1) % hexColorCodes.Length;
        targetColor = HexToColor(hexColorCodes[currentIndex]);
        transitionTimer = 0f;
        isTransitioning = true;
    }

    private void UpdateColorTransition()
    {
        transitionTimer += Time.deltaTime;
        float t = Mathf.Clamp01(transitionTimer / transitionDuration);
        Color newColor = Color.Lerp(initialColor, targetColor, t);
        mainCamera.backgroundColor = newColor;

        if (t >= 1f)
        {
            isTransitioning = false;
            initialColor = targetColor;
        }
    }

    private Color HexToColor(string hexCode)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString(hexCode, out color);
        return color;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WhatPairUI : MonoBehaviour
{
    string[] primaryColors = { "WHITE", "RED", "BLACK", "YELLOW", "VIOLET" };
    string[] secondaryColors = { "BLUE", "ORANGE", "GREEN", "BROWN", "SLATE" };

    Hashtable copperColors = new Hashtable();

    [Header("App Themes")]
    [SerializeField] Color32 lightColor;
    [SerializeField] Color32 darkColor;

    [Header("UI Objects")]
    [SerializeField] GameObject bgPanel;
    [SerializeField] GameObject brightThemeToggle;
    [SerializeField] GameObject darkThemeToggle;
    [SerializeField] Text pairColorsText;
    [SerializeField] Text pairNumbersText;
    [SerializeField] Image primaryWire;
    [SerializeField] Image secondaryWire;

    private bool darkThemeEnabled;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize hashtable
        copperColors.Add("WHITE", new Color32(255, 255, 255, 255));
        copperColors.Add("RED", new Color32(255, 0, 0, 255));
        copperColors.Add("BLACK", new Color32(15, 15, 15, 255));
        copperColors.Add("YELLOW", new Color32(222, 209, 60, 255));
        copperColors.Add("VIOLET", new Color32(162, 87, 190, 255));
        copperColors.Add("BLUE", new Color32(56, 122, 233, 255));
        copperColors.Add("ORANGE", new Color32(243, 139, 0, 255));
        copperColors.Add("GREEN", new Color32(100, 215, 80, 255));
        copperColors.Add("BROWN", new Color32(144, 98, 55, 255));
        copperColors.Add("SLATE", new Color32(122, 122, 132, 255));

        // Initialize Scene
        darkThemeEnabled = true;
        pairColorsText.gameObject.SetActive(false);
        pairNumbersText.gameObject.SetActive(false);
        primaryWire.gameObject.SetActive(false);
        secondaryWire.gameObject.SetActive(false);

        ChangeTheme(darkThemeEnabled);
    }

    public void InputReceived(string inputString)
    {
        int inputInt = 0;

        if (inputString.Length > 0)
        {
            inputInt = int.Parse(inputString);
        }

        if (inputInt > 0)
        {
            if (inputInt > 25)
            {
                inputInt = CalculateValidPairNumber(inputInt);
            }

            string primaryColor = CalculatePrimaryColor(inputInt);
            string secondaryColor = CalculateSecondaryColor(inputInt.ToString());

            pairColorsText.text = $"{primaryColor}-{secondaryColor}";
            pairColorsText.gameObject.SetActive(true);

            pairNumbersText.text = inputInt.ToString();
            pairNumbersText.gameObject.SetActive(true);

            primaryWire.color = (Color32)copperColors[primaryColor];
            secondaryWire.color = (Color32)copperColors[secondaryColor];

            primaryWire.gameObject.SetActive(true);
            secondaryWire.gameObject.SetActive(true);
        }
        else
        {
            pairColorsText.gameObject.SetActive(false);
            pairNumbersText.gameObject.SetActive(false);
            primaryWire.gameObject.SetActive(false);
            secondaryWire.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Translates numbers greater than 25 to within the 0-25 range using integer division.
    /// This is because copper colors are only coded from 0-25.
    /// Example: 132 / 25 = 5, 132 - (25 * 5) = 7.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private int CalculateValidPairNumber(int num)
    {
        // If num is an even multiple of 25, return 25
        if (num % 25 == 0)
        {
            return 25;
        }
        // Return the valid pair number
        else
        {
            return num - (25 * (num / 25));
        }
    }

    /// <summary>
    /// Sets the primary color depending on pair number.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private string CalculatePrimaryColor(int num)
    {
        // White
        if (num > 0 && num <= 5)
        {
            return primaryColors[0];
        }
        // Red
        else if (num > 5 && num <= 10)
        {
            return primaryColors[1];
        }
        // Black
        else if (num > 10 && num <= 15)
        {
            return primaryColors[2];
        }
        // Yellow
        else if (num > 15 && num <= 20)
        {
            return primaryColors[3];
        }
        // Violet
        else if (num > 20 && num <= 25)
        {
            return primaryColors[4];
        }

        return "";
    }

    /// <summary>
    /// Finds last digit in input and returns the corresponding color.
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    private string CalculateSecondaryColor(string num)
    {
        // Get the last digit of the input number
        int lastDigit = int.Parse(num[^1].ToString());

        // Return the correct secondary color
        // Blue
        if (lastDigit == 1 || lastDigit == 6)
        {
            return secondaryColors[0];
        }
        // Orange
        else if (lastDigit == 2 || lastDigit == 7)
        {
            return secondaryColors[1];
        }
        // Green
        else if (lastDigit == 3 || lastDigit == 8)
        {
            return secondaryColors[2];
        }
        // Brown
        else if (lastDigit == 4 || lastDigit == 9)
        {
            return secondaryColors[3];
        }
        // Slate
        else if (lastDigit == 5 || lastDigit == 0)
        {
            return secondaryColors[4];
        }

        return "";
    }

    /// <summary>
    /// Toggles bright/dark theme and changes UI colors.
    /// </summary>
    public void ChangeTheme(bool newTheme)
    {
        // Toggle dark theme
        darkThemeEnabled = newTheme;

        // Dark theme enabled
        if (darkThemeEnabled)
        {
            bgPanel.GetComponent<Image>().color = darkColor;
            foreach(Text child in bgPanel.GetComponentsInChildren<Text>())
            {
                child.color = lightColor;
            }
            brightThemeToggle.SetActive(true);
            darkThemeToggle.SetActive(false);
        }
        // Bright theme enabled
        else
        {
            bgPanel.GetComponent<Image>().color = lightColor;
            foreach (Text child in bgPanel.GetComponentsInChildren<Text>())
            {
                child.color = darkColor;
            }
            brightThemeToggle.SetActive(false);
            darkThemeToggle.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class ShipSelctionMenu : MonoBehaviour
{
    public Image shipPreviewImage; // UI Image to preview selected ship
    public Material shipMaterial; // Reference to the ship's material

    private Color[] colorOptions = {
        new Color(0, 0, 1),     // Blue
        new Color(1, 0, 1),     // Pink
        new Color(0, 1, 0),     // Green
        new Color(1, 1, 1),     // White
        new Color(1, 0, 0),     // Red
        new Color(1, 1, 0),     // Yellow
        new Color(1, 0.5f, 0),  // Orange
        new Color(0.6f, 0.3f, 0),  // Brown
        new Color(0.5f, 0, 0.5f)   // Purple
    }; // Array of predefined colors

    private int currentColorIndex = 0; // Index of the current color

    AudioManager audiomg;

    private void Start()
    {
        audiomg = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Display the first ship color as default preview
        UpdateShipPreview();
    }

    // Called when the player presses a button to change ship color
    public void OnChangeColorButtonPressed()
    {
        // Get the next color from the array
        Color newColor = colorOptions[currentColorIndex];

        // Update ship color
        ChangeShipColor(newColor);

        // Increment color index and wrap around if necessary
        currentColorIndex = (currentColorIndex + 1) % colorOptions.Length;

        Debug.Log("Current color index: " + currentColorIndex);
        audiomg.PlaySFX(audiomg.OnClicked);
        UpdateShipPreview();
    }

    // Changes the ship's color
    private void ChangeShipColor(Color newColor)
    {
        shipMaterial.color = newColor;
    }

    // Method to update ship preview image based on selected ship index
    void UpdateShipPreview()
    {
        // Update the ship preview image color
        shipPreviewImage.color = shipMaterial.color;
    }

    public void SaveShipColor()
    {
        
        // Get the current color of the ship material
        Color color = shipMaterial.color;

        audiomg.PlaySFX(audiomg.OnClicked);
        // Save the ship color to PlayerPrefs
        PlayerPrefs.SetFloat("ShipColor_R", color.r);
        PlayerPrefs.SetFloat("ShipColor_G", color.g);
        PlayerPrefs.SetFloat("ShipColor_B", color.b);
        PlayerPrefs.SetFloat("ShipColor_A", color.a);
    }

}

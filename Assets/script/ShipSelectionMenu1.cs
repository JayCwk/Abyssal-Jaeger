using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelectionMenu1 : MonoBehaviour
{
    public Sprite[] shipSprites; // Array of ship sprites representing different designs
    public Image shipPreviewImage; // UI Image to preview selected ship

    public int selectedShipIndex1 ; // Index of currently selected ship

    void Start()
    {
        // Display the first ship variant as default preview
        UpdateShipPreview();
    }

    // Method to update ship preview image based on selected ship index
    void UpdateShipPreview()
    {
        shipPreviewImage.sprite = shipSprites[selectedShipIndex1];
    }

    // Method to handle next ship button click
    public void NextShip()
    {
        // Increment selected ship index
        selectedShipIndex1 = (selectedShipIndex1 + 1) % shipSprites.Length;
        // Update ship preview
        UpdateShipPreview();
    }

    public void ConfirmSelection()
    {
        // Save player's ship design preference (optional)
        PlayerPrefs.SetInt("SelectedShipIndex1", selectedShipIndex1);
        // Load game scene or perform other actions


        Debug.Log("Confirmed index1: " + selectedShipIndex1);
    }
}

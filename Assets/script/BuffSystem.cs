using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BuffSystem;

// Define a struct to represent a buff
public struct Buff
{
    public BuffType type;
    public float duration;
    // Add any other relevant properties
}

public class BuffSystem : MonoBehaviour
{
   // Define a struct to represent a buff
    public struct Buff
    {
        public BuffType type;
        public float duration;
        // Add any other relevant properties
    }

    // Define buff types
    public enum BuffType { TripleShot, Shield }

    // List to store active buffs
    private List<Buff> activeBuffs = new List<Buff>();

    // Reference to the player controller or any other script that handles the player's behavior
    public PlayerCtrl playerController;

    // Apply a buff to the player
    public void ApplyBuff(BuffType type, float duration)
    {
        // Create a new buff instance
        Buff buff = new Buff();
        buff.type = type;
        buff.duration = duration;

        // Apply the buff effect based on its type
        switch (type)
        {
            case BuffType.TripleShot:
                // Apply the TripleShot buff effect
                playerController.EnableTripleShot();
                break;
            case BuffType.Shield:
                // Apply the Shield buff effect
                playerController.ActivateShield();
                break;
                // Add cases for other buff types
        }

        // Add the buff to the list of active buffs
        activeBuffs.Add(buff);

        // Start the coroutine to manage the buff duration
        StartCoroutine(ManageBuffDuration(buff));
    }

    // Coroutine to manage the duration of a buff
    IEnumerator ManageBuffDuration(Buff buff)
    {
        yield return new WaitForSeconds(buff.duration);

        // Remove the buff effect
        switch (buff.type)
        {
            case BuffType.TripleShot:
                // Remove the TripleShot buff effect
                playerController.DisableTripleShot();
                break;
            case BuffType.Shield:
                // Remove the Shield buff effect
                playerController.DeactivateShield();
                break;
                // Add cases for other buff types
        }

        // Remove the buff from the list of active buffs
        activeBuffs.Remove(buff);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Handle interaction with the player
            PlayerCtrl player = other.GetComponent<PlayerCtrl>();
            if (player != null)
            {
                // Generate a random index to select a random buff type from the BuffType enum
                int randomIndex = Random.Range(0, System.Enum.GetValues(typeof(BuffType)).Length);
                BuffType randomBuffType = (BuffType)randomIndex;

                // Apply the randomly selected buff to the player
                ApplyBuff(randomBuffType, 10f); // Adjust duration as needed
            }
        }
    }
}

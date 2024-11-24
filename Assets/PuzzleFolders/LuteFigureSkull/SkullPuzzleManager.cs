using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPuzzleManager : MonoBehaviour
{
    
    public List<GameObject> skulls; // Assign your skull GameObjects in the Inspector
    public bool puzzleActivated = false;

    public List<int> correctOrder = new List<int>(); // Assign the correct order in Inspector (0-based index)

    private List<int> playerOrder = new List<int>(); // Tracks the player's inputs

    void Start()
    {
        // Assign IDs to skulls
        for (int i = 0; i < skulls.Count; i++)
        {
            Skull skull = skulls[i].GetComponent<Skull>();
            if (skull == null)
            {
                Debug.LogError($"Skull {skulls[i].name} is missing the Skull script!");
                continue;
            }

            skull.AssignID(i);
            skull.OnSkullClicked += HandleSkullClick; // Subscribe to skull click events
        }
    }

    private void HandleSkullClick(int skullID)
    {
        if (!puzzleActivated)
        {
            Debug.Log("The puzzle is not activated yet!");
            ResetPuzzle();
            return;
        }

        // Add the clicked skull to the player's order
        playerOrder.Add(skullID);

        // Check if the player input matches the correct order
        if (!IsOrderCorrect())
        {
            // Incorrect order, reset the puzzle
            Invoke("ResetPuzzle", 1f); // Reset after a short delay
            return;
        }

        // If the player completes the sequence correctly
        if (playerOrder.Count == correctOrder.Count)
        {
            Debug.Log("Puzzle Solved!");
            PuzzleSolved();
        }
    }

    private bool IsOrderCorrect()
    {
        // Check if player's order matches the correct order so far
        for (int i = 0; i < playerOrder.Count; i++)
        {
            if (playerOrder[i] != correctOrder[i])
            {
                return false;
            }
        }
        return true;
    }

    private void ResetPuzzle()
    {
        Debug.Log("Puzzle Reset");
        playerOrder.Clear();

        // Optionally, reset visual effects on all skulls
        foreach (var skull in skulls)
        {
            Skull skullScript = skull.GetComponent<Skull>();
            if (skullScript != null)
            {
                skullScript.ResetVisuals();
            }
        }
    }

    private void PuzzleSolved()
    {
        // Trigger the win state
        Debug.Log("You solved the puzzle!");
        // Add win logic here (e.g., unlock a door, spawn an item, etc.)
    }

    public void ActivatePuzzle()
    {
        puzzleActivated = true;
        Debug.Log("The puzzle has been activated!");
    }

}

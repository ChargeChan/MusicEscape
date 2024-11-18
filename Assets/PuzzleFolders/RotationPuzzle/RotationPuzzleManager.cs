using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleManager : MonoBehaviour
{
    public RotationPuzzle[] squares; // References to each square
    public Animator puzzleCompleteAnimator; // Animator for the win animation
    private bool puzzleComplete = false;
    private bool hasScrambled = false; // Flag to ensure scrambling happens before checks

    private void Start()
    {
        ScrambleRotations();
        hasScrambled = true; // Set flag to true after scrambling
    }

    private void Update()
    {
        if (puzzleComplete || !hasScrambled) return; // Skip checks until scrambling is complete

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                RotationPuzzle square = hit.collider.GetComponent<RotationPuzzle>();
                if (square != null)
                {
                    square.RotateSquare();
                    CheckPuzzleCompletion();
                }
            }
        }
    }

    private void CheckPuzzleCompletion()
    {
        // Check if all squares are correctly aligned
        foreach (RotationPuzzle square in squares)
        {
            if (!square.IsCorrectlyAligned())
            {
                return; // If any square is misaligned, exit early
            }
        }

        // If all squares are correctly aligned, trigger win condition
        puzzleComplete = true;
        puzzleCompleteAnimator.SetTrigger("PuzzleComplete");
        LockSquares();
 
    }

    private void LockSquares()
    {
        foreach (RotationPuzzle square in squares)
        {
            square.LockRotation();

        }
    }

    public void ScrambleRotations()
    {
        foreach (RotationPuzzle square in squares)
        {
            // Generate a random number (1, 2, or 3) and multiply by 90 for scrambling (avoid 0 to ensure misalignment)
            int randomAngle = Random.Range(1, 4) * 90;
            Quaternion scrambledRotation = square.transform.rotation * Quaternion.Euler(randomAngle, 0, 0); // Offset the current rotation

            square.SetScrambledRotation(scrambledRotation); // Apply scrambled rotation safely
        }

        // Validate that all squares are scrambled and not initially aligned
        foreach (RotationPuzzle square in squares)
        {
            if (square.IsCorrectlyAligned())
            {
                Debug.LogWarning("Square is aligned after scrambling! Rescrambling...");
                ScrambleRotations();
                return; // Restart scrambling if alignment is detected
            }
        }
    }
}

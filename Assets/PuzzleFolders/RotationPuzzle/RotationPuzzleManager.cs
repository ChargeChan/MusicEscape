using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzleManager : MonoBehaviour
{
    public RotationPuzzle[] squares; // References to the 3 rotatable squares
    public Animator puzzleCompleteAnimator; // Animator for the win animation
    public GameObject fourthPiece; // Reference to the fourth piece
    private bool puzzleComplete = false;
    private bool fourthPiecePlaced = false; // Flag to check if the fourth piece is placed

    private void Start()
    {
        ScrambleRotations();
    }

    private void Update()
    {
        if (puzzleComplete) return;

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
        // Check if all rotatable squares are correctly aligned
        foreach (RotationPuzzle square in squares)
        {
            if (!square.IsCorrectlyAligned())
            {
                return; // If any square is misaligned, exit early
            }
        }

        // Check if the fourth piece is placed
        if (fourthPiecePlaced)
        {
            // Trigger the win condition
            puzzleComplete = true;
            puzzleCompleteAnimator.SetTrigger("PuzzleComplete");
            LockSquares();
            Debug.Log("Puzzle Complete!!!");
        }
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
            int randomAngle = Random.Range(1, 4) * 90;
            Quaternion scrambledRotation = square.transform.rotation * Quaternion.Euler(randomAngle, 0, 0);
            square.SetScrambledRotation(scrambledRotation);
        }
    }

    public void PlaceFourthPiece()
    {
        // Mark the fourth piece as placed and check completion
        fourthPiecePlaced = true;
        CheckPuzzleCompletion();
    }
}

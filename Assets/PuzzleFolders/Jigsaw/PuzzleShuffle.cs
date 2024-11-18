using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleShuffle : MonoBehaviour
{
    public GameObject puzzleParent;  // The parent GameObject containing all puzzle pieces
    public Vector3 shuffleAreaCenter; // The center point of the shuffle area
    public Vector3 shuffleAreaSize;   // The size of the shuffle area (Width, Height, Depth)

    private void Start()
    {
        ShufflePieces();
    }

    private void ShufflePieces()
    {
        // Check if puzzleParent is set
        if (puzzleParent == null)
        {
            Debug.LogError("Puzzle Parent is not assigned!");
            return;
        }

        // Get all puzzle pieces from the parent
        List<Transform> pieces = new List<Transform>();
        foreach (Transform piece in puzzleParent.transform)
        {
            if (piece.CompareTag("Jigsaw")) // Make sure the piece is a puzzle piece
            {
                pieces.Add(piece);
            }
        }

        // Shuffle the pieces randomly within the specified area
        for (int i = 0; i < pieces.Count; i++)
        {
            Transform piece = pieces[i];

            // Generate a random position within the shuffle area
            float randomX = Random.Range(shuffleAreaCenter.x - shuffleAreaSize.x / 2, shuffleAreaCenter.x + shuffleAreaSize.x / 2);
            float randomZ = Random.Range(shuffleAreaCenter.z - shuffleAreaSize.z / 2, shuffleAreaCenter.z + shuffleAreaSize.z / 2);

            // Keep the Y position of the pieces the same (or change it if needed)
            float randomY = piece.position.y;

            Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);
            piece.position = randomPosition; // Apply the new position
        }

        Debug.Log("Puzzle pieces shuffled within the defined area!");
    }
}

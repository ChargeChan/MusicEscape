using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class piecePlacer : NeedItemScript
{
    public GameObject harpPiece;
    public RotationPuzzleManager puzzleManager; // Reference to the puzzle manager

    void Start()
    {
        if (puzzleManager == null)
        {
            puzzleManager = FindObjectOfType<RotationPuzzleManager>();
        }
    }

    public override void Open()
    {
        harpPiece.gameObject.SetActive(true);
        SendMessageUpwards(harpPiece.name + "Done");
        Debug.Log(harpPiece.name + "Done");

        // Notify the puzzle manager
        if (puzzleManager != null)
        {
            puzzleManager.PlaceFourthPiece();
        }
    }
}

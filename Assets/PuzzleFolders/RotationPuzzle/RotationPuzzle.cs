using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPuzzle : MonoBehaviour
{
    private Quaternion correctRotation; // The correct rotation for this square
    private Quaternion currentRotation; // Tracks the current scrambled rotation

    private void Start()
    {
        correctRotation = transform.rotation; // Save the correct rotation at start
    }

    public bool IsCorrectlyAligned()
    {
        return Quaternion.Angle(transform.rotation, correctRotation) < 1f; // Check alignment
    }

    public void RotateSquare()
    {
        transform.Rotate(90, 0, 0); // Rotate the square by 90 degrees on X
    }

    public void LockRotation()
    {
        transform.rotation = correctRotation; // Snap to correct rotation
    }

    public void SetScrambledRotation(Quaternion scrambledRotation)
    {
        transform.rotation = scrambledRotation;
        currentRotation = scrambledRotation; // Track scrambled rotation for reference
    }

}

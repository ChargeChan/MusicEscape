using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class SkullPuzzleManager : MonoBehaviour
{
    
    public List<GameObject> skulls;
    public bool puzzleActivated = false;
    private bool puzzleComplete = false;
    public Animator puzzleCompleteAnimator;
    public AudioSource audioSource;

    public List<int> correctOrder = new List<int>(); 

    private List<int> playerOrder = new List<int>(); // player's inputs
    public MidiStreamPlayer midiStreamPlayer;
    private MPTKEvent mptkEvent;

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

        audioSource = GetComponent<AudioSource>();
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

    private void Update()
    {
        if (puzzleComplete) return;
    }

    private void PuzzleSolved()
    {
        Debug.Log("You solved the puzzle!");
        puzzleComplete = true;
        audioSource.Play();
        puzzleCompleteAnimator.SetTrigger("PuzzleComplete");

    }

    public void ActivatePuzzle()
    {
        puzzleActivated = true;
        Debug.Log("The puzzle has been activated!");
    }

    public void PlayNote(int note)
    {
        mptkEvent = new MPTKEvent() { Value = note };
        midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
    }

    private IEnumerator SetInstrument()
    {
        yield return new WaitForSeconds(1);
        MPTKEvent PatchChange = new MPTKEvent()
        {
            Command = MPTKCommand.PatchChange,
            Value = 19, // pipe organ
            Channel = 3,
            Duration = 10
        }; // Instrument are defined by channel (from 0 to 15). So at any time, only 16 differents instruments can be used simultaneously.
        midiStreamPlayer.MPTK_PlayEvent(PatchChange);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using UnityEditor.Experimental.GraphView;

public class PipeCanvasScript : MonoBehaviour
{
    public MidiStreamPlayer midiStreamPlayer;
    private MPTKEvent mptkEvent;
    public GameObject wallRunePuzzle;
    public GameObject wheelPuzzle;
    public GameObject pillarPuzzle;
    private GameObject music;
    private Canvas myCanvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetInstrument());
        myCanvas = GetComponent<Canvas>();
        music = GameObject.Find("Music");
    }

    public void OpenCanvas()
    {
        music.SendMessage("Quiet"); // we want the BGM to be quiet for the instrument to play
        myCanvas.enabled = true;
    }

    public void CloseCanvas()
    {
        music.SendMessage("UnQuiet");
        myCanvas.enabled = false;
    }


    public void PlayNote(int note)
    {
        mptkEvent = new MPTKEvent() { Value = note , Channel = 2};
        midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
        if(GameManager.Instance.GetCurrentCameraIndex() == 21) // wall rune puzzle
        {
            //wallRunePuzzle.BroadcastMessage("PlayNote", note);
            pillarPuzzle.BroadcastMessage("PlayNote", note);
        }
        else if(GameManager.Instance.GetCurrentCameraIndex() == 22)
        {
            wheelPuzzle.BroadcastMessage("PlayNote", note);
        }
        else if (GameManager.Instance.GetCurrentCameraIndex() == 21)
        {
            //pillarPuzzle.BroadcastMessage("PlayNote", note);
        }
    }

    public void StopNote() 
    {
        midiStreamPlayer.MPTK_StopEvent(mptkEvent);
        if (GameManager.Instance.GetCurrentCameraIndex() == 21) // wall rune puzzle
        {
            // wallRunePuzzle.BroadcastMessage("TurnOff");
            pillarPuzzle.BroadcastMessage("TurnOff");
        }
        if (GameManager.Instance.GetCurrentCameraIndex() == 22) 
        {
            wheelPuzzle.BroadcastMessage("StopNote");
        }
        if (GameManager.Instance.GetCurrentCameraIndex() == 21)
        {
            
        }
    }

    // midiStreamPlayer takes a second to load so a buffer is needed
    private IEnumerator SetInstrument()
    {
        yield return new WaitForSeconds(1);
        MPTKEvent PatchChange = new MPTKEvent()
        {
            Command = MPTKCommand.PatchChange,
            Value = 75, 
            Channel = 2
        }; // Instrument are defined by channel (from 0 to 15). So at any time, only 16 differents instruments can be used simultaneously.
        midiStreamPlayer.MPTK_PlayEvent(PatchChange);
    }
}

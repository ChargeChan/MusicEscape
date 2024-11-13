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
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetInstrument());
    }


    public void PlayNote(int note)
    {
        mptkEvent = new MPTKEvent() { Value = note , Channel = 2};
        midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
        if(GameManager.Instance.GetCurrentCameraIndex() == 21) // wall rune puzzle
        {
            wallRunePuzzle.BroadcastMessage("PlayNote", note);
        }
        else if(GameManager.Instance.GetCurrentCameraIndex() == 22)
        {
            wheelPuzzle.BroadcastMessage("PlayNote", note);
        }
    }

    public void StopNote() 
    {
        midiStreamPlayer.MPTK_StopEvent(mptkEvent);
        if (GameManager.Instance.GetCurrentCameraIndex() == 21) // wall rune puzzle
        {
            wallRunePuzzle.BroadcastMessage("TurnOff");
        }
        if (GameManager.Instance.GetCurrentCameraIndex() == 22) 
        {
            wheelPuzzle.BroadcastMessage("StopNote");
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

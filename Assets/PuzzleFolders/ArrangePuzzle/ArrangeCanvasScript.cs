using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using MidiPlayerTK;

public class ArrangeCanvasScript : MonoBehaviour
{
    public GameObject[] slots = new GameObject[6];
    public GameObject[] blocks = new GameObject[6];
    List<int> notes = new List<int>();
    //public ArrangeBlockScript[] blocks;

    private int blockMoving;
    private bool isBlockMoving = false;
    private string shiftDirection;
    private bool shifted = false;
    private bool isPlaying = false;

    public MidiStreamPlayer midiStreamPlayer;
    private MPTKEvent mptkEvent;
    // Start is called before the first frame update
    void Start()
    {
        
        for (int i=0;  i<slots.Length; i++)
        {
            blocks[i].SendMessage("SetIndex", i);
            //blocks[i].transform.position = slots[i].transform.position;
        }
        StartCoroutine(SetInstrument());
    }

    public void SetPositions()
    {
        Debug.Log("enable");
        for (int i = 0; i < slots.Length; i++)
        {
            //blocks[i].transform.position = slots[i].transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {//moving code
        if (!shifted)
        {
            if (isBlockMoving)
            {
                if (blockMoving > 0)
                    if (blocks[blockMoving].transform.position.x < blocks[blockMoving - 1].transform.position.x)
                    {
                        blocks[blockMoving - 1].transform.position = slots[blockMoving].transform.position;
                        shiftDirection = "left";
                        SwapIndex(blockMoving, blockMoving - 1);
                        shifted = true;
                    }
                if (blockMoving < blocks.Length - 1)
                {
                    if (blocks[blockMoving].transform.position.x > blocks[blockMoving + 1].transform.position.x)
                    {
                        blocks[blockMoving + 1].transform.position = slots[blockMoving].transform.position;
                        shiftDirection = "right";
                        SwapIndex(blockMoving, blockMoving + 1);
                        shifted = true;
                    }
                }
            }
        }
        
    }

    //block has detected that it is being dragged
    public void BlockDrag(int x)
    {
        blockMoving = x;
        isBlockMoving = true;
        //Debug.Log(blockMoving + " " + isBlockMoving);
    }

    //block has detected it is no longer being dragged
    public void BlockDrop()
    {
        if (shiftDirection == "left")
        {
            blocks[blockMoving-1].transform.position = slots[blockMoving-1].transform.position;
            
            blocks[blockMoving-1].SendMessage("SetIndex", blockMoving - 1);
            blocks[blockMoving].SendMessage("SetIndex", blockMoving);
            shiftDirection = "none";
        }
        else if (shiftDirection == "right")
        {
            blocks[blockMoving + 1].transform.position = slots[blockMoving + 1].transform.position;

            blocks[blockMoving + 1].SendMessage("SetIndex", blockMoving + 1);
            blocks[blockMoving].SendMessage("SetIndex", blockMoving);
            shiftDirection = "none";
        }
        else
        {
            blocks[blockMoving].transform.position = slots[blockMoving].transform.position;
        }
        isBlockMoving=false;
        shifted = false;
    }

    public void SwapIndex(int x, int y)
    {
        GameObject temp = blocks[x];
        blocks[x] = blocks[y];
        blocks[y] = temp;
    }

    public void EnterNote(int note)
    {
        notes.Add(note);
    }

    public void GatherNotes()
    {
        if (isPlaying) return;
        notes.Clear();
        Debug.ClearDeveloperConsole();
        for (int i = 0; i < blocks.Length; i++)
        {
            blocks[i].SendMessage("SendNoteUp");
        }
        StartCoroutine(WaitForNotes());
    }

    private void CheckSolution()
    {
        for (int i = 1; i < notes.Count; i++)
        {
            if (notes[i - 1] > notes[i])
            {
                //Debug.Log("Failed " + notes[i - 1] + ">" + notes[i]);
                return;
            }
        }
        Debug.Log("Correct solution");
        StartCoroutine(PlayCorrectChime());
    }

    IEnumerator WaitForNotes()
    {
        yield return new WaitForSeconds(0.3f);
        for(int i=0;  i < notes.Count; i++)
        {
            //Debug.Log(notes[i]);
        }
        StartCoroutine(PlayAllNotes());
    }

    private IEnumerator SetInstrument()
    {
        yield return new WaitForSeconds(1);
        MPTKEvent PatchChange = new MPTKEvent()
        {
            Command = MPTKCommand.PatchChange,
            Value = 46,
            Channel = 1
        }; // Instrument are defined by channel (from 0 to 15). So at any time, only 16 differents instruments can be used simultaneously.
        midiStreamPlayer.MPTK_PlayEvent(PatchChange);
    }

    private IEnumerator PlayAllNotes()
    {
        isPlaying = true;
        //yield return new WaitForSeconds(1);
        //play first F
        mptkEvent = new MPTKEvent() { Value = 65, Duration = 300, Channel = 1 };
        midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0;i < notes.Count;i++)
        {
            mptkEvent = new MPTKEvent() { Value = notes[i], Duration = 300, Channel = 1 };
            midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
            yield return new WaitForSeconds(0.5f);
        }

        mptkEvent = new MPTKEvent() { Value = 65+12, Duration = 300, Channel = 1 };
        midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
        CheckSolution();
        isPlaying = false;
    }

    private IEnumerator PlayCorrectChime()
    {
        int[] correctChimeNotes = { 65, 67, 68, 70, 72, 73, 75, 77 };
        yield return new WaitForSeconds(0.8f);
        for(int i = 0; i < correctChimeNotes.Length;i++)
        {
            mptkEvent = new MPTKEvent() { Value = correctChimeNotes[i], Duration = 100, Channel = 1 };
            midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
            yield return new WaitForSeconds(0.05f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using Unity.VisualScripting;

public class PianoCanvasScript : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject[] chordProgress;
    private int slotsCounter = 0;
    private string currentChord = "";
    [SerializeField] private string totalChords = "";
    private int noteCounterForSingle = 0;
    private int chordCounterTotal = 0;
    

    public MidiStreamPlayer midiStreamPlayer;
    private MPTKEvent mptkEvent;
    private GameObject music;
    private Canvas myCanvas;
    private Dictionary<string, string> chordNameMap;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetInstrument());
        music = GameObject.Find("Music");
        myCanvas = gameObject.GetComponent<Canvas>();
        chordNameMap = new Dictionary<string, string>
        {
            {"F5G5#C6", "Fm" }
        };
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
        mptkEvent = new MPTKEvent() { Value = note };
        midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
        ChordPlayHandler(note);
    }

    public void ChordPlayHandler(int note)
    {
        currentChord += MidiPlayerTK.HelperNoteLabel.LabelFromMidi(note);
        chordProgress[noteCounterForSingle].SendMessage("TurnOn");
        noteCounterForSingle++;
        if (noteCounterForSingle >= 3) // reset all lights
        {
            StartCoroutine(ChordPlayed());
            totalChords += IdentifyChord( currentChord); // identify chord name based on notes
            currentChord = "";
            slotsCounter++;
            
        }
    }

    private string IdentifyChord(string chord)
    {
        string chordName = "Cm";
        //identify from dictionary
        slots[slotsCounter].SendMessage("SetChord", chordName);
        return chordName;
    }


    public void StopNote(){midiStreamPlayer.MPTK_StopEvent(mptkEvent);}


    public void CheckPassword()
    {
        
    }

    public void Reset()
    {
        currentChord = "";
        slotsCounter = 0;
        totalChords = "";
        noteCounterForSingle = 0;
        chordCounterTotal = 0;
        for (int i = 0; i < chordProgress.Length; i++)
        {
            chordProgress[i].SendMessage("TurnOff"); //the lights turn off to show that they can be used again
        }
    }

    private IEnumerator ChordPlayed()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < chordProgress.Length; i++)
        {
            chordProgress[i].SendMessage("ColorSetting"); //the lights display other color to show that they've been set
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < chordProgress.Length; i++)
        {
            chordProgress[i].SendMessage("TurnOff"); //the lights turn off to show that they can be used again
        }
        noteCounterForSingle = 0;
        
    }

    // midiStreamPlayer takes a second to load so a buffer is needed
    private IEnumerator SetInstrument()
    {
       yield return new WaitForSeconds(1);
        MPTKEvent PatchChange = new MPTKEvent()
        {
            Command = MPTKCommand.PatchChange,
            Value = 19, // pipe organ
            Channel = 0
        }; // Instrument are defined by channel (from 0 to 15). So at any time, only 16 differents instruments can be used simultaneously.
        midiStreamPlayer.MPTK_PlayEvent(PatchChange);
    }

}

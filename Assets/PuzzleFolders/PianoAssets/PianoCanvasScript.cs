using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class PianoCanvasScript : MonoBehaviour
{
    public GameObject[] slots;
    public GameObject[] chordProgress;
    private int slotsCounter = 0;
    private string currentChord = "";
    [SerializeField] private string totalChords = "";
    private int noteCounterForSingle = 0;
    private int chordCounterTotal = 0;
    private bool keysLocked = false;
    

    public MidiStreamPlayer midiStreamPlayer;
    public MidiFilePlayer midiFilePlayer;
    private MPTKEvent mptkEvent;
    private GameObject music;
    private Canvas myCanvas;
    private Dictionary<string, string> chordNameMap;
    private string correctChords;
    private MPTKEvent[] chordEvents;
    public long chordPlaybackDuration;
    public Animator animator;
    public Animator cameraAnimator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetInstrument());
        music = GameObject.Find("Music");
        myCanvas = gameObject.GetComponent<Canvas>();
        chordEvents = new MPTKEvent[3];
        chordNameMap = new Dictionary<string, string>
        {
            {"F5G5#C6", "Fm" },
            {"C5E5G5", "Cm" },
            {"F5A5#C6#",  "Bbm"},
            {"F5#A5#D6#",  "Ebm"},
            {"G5#B5D6#",  "Abm"},
            {"G5#C6#E6",  "Dbm"},
            {"F5#A5C6#", "F#m" },
            {"F5#B5D6", "Bm" },
            {"G5B5E6", "Em" },
            {"A5C6E6", "Am" },
            {"F5A5D6", "Dm" },
            {"G5A5#D6", "Gm" },
        };
        correctChords = "FmDbmAbmEbm";
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
        if (keysLocked) return;
        ChordPlayHandler(note);
    }

    public void ChordPlayHandler(int note)
    {
        currentChord += MidiPlayerTK.HelperNoteLabel.LabelFromMidi(note);
        chordEvents[noteCounterForSingle] = new MPTKEvent() { Value = note, Velocity = 50 };
        chordProgress[noteCounterForSingle].SendMessage("TurnOn");
        noteCounterForSingle++;
        if (noteCounterForSingle >= 3) // reset all lights
        {
            StartCoroutine(ChordPlayed());
            
            totalChords += IdentifyChord( currentChord); // identify chord name based on notes
            Debug.Log(currentChord);
            currentChord = "";
            slotsCounter++;
            
        }
        if(chordCounterTotal == slots.Length)
        {
            CheckPassword();
        }
    }

    private string IdentifyChord(string chord)
    {
        chordCounterTotal++;
        string chordName = "";
        //identify from dictionary
        if (chordNameMap.ContainsKey(chord))
        {
            chordName = chordNameMap[chord];
        }
        else
        {
            chordName = "?";
        }
            
        slots[slotsCounter].SendMessage("SetChord", chordName);
        return chordName;
    }


    public void StopNote(){midiStreamPlayer.MPTK_StopEvent(mptkEvent);}


    public void CheckPassword()
    {
        string password = totalChords;
        if(password == correctChords)
        {
            //play correct chords
            Debug.Log("correct");
            keysLocked = true;
            StartCoroutine(PlaySolution());
        }
        else
        {
            //show password is wrong
            Reset();
        }
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
        for(int i = 0;i<slots.Length;i++)
        {
            slots[i].SendMessage("SetChord", "");
        }
    }

    private IEnumerator ChordPlayed()
    {
        keysLocked = true;

        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < chordProgress.Length; i++)
        {
            chordProgress[i].SendMessage("ColorSetting"); //the lights display other color to show that they've been set
        }
        yield return new WaitForSeconds(0.2f);
        for (int i=0; i<chordEvents.Length; i++)
        {
            // play all notes in chord
            midiStreamPlayer.MPTK_PlayEvent(chordEvents[i]); 
        }
        yield return new WaitForSeconds(1.3f);
        
        for (int i = 0; i < chordEvents.Length; i++)
        {
            // stop all notes in chord
            midiStreamPlayer.MPTK_StopEvent(chordEvents[i]);
        }
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < chordProgress.Length; i++)
        {
            chordProgress[i].SendMessage("TurnOff"); //the lights turn off to show that they can be used again
        }
        noteCounterForSingle = 0;
        keysLocked = false;
    }

    private IEnumerator PlaySolution()
    {
        yield return new WaitForSeconds(2f);
        midiFilePlayer.MPTK_Play();
        yield return new WaitForSeconds(3.5f);
        myCanvas.enabled = false;
        CamManager camManager = FindObjectOfType<CamManager>();
        camManager.SetActiveCamera(8);
        animator.SetTrigger("Move");
        yield return new WaitForSeconds(1f);
        cameraAnimator.SetTrigger("Move");
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(2);

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

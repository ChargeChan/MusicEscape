using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiPlayerTK;

public class PianoCanvasScript : MonoBehaviour
{
    public GameObject[] slots = new GameObject[5];
    private int codeCounter = 0;
    private string codeEntered = "";
    private string runePassword = "CrystalCrystalCrystalCrystalCrystal";

    public MidiStreamPlayer midiStreamPlayer;
    private MPTKEvent mptkEvent;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetInstrument());
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayNote(int note)
    {
        mptkEvent = new MPTKEvent() { Value = note };
        midiStreamPlayer.MPTK_PlayEvent(mptkEvent);
    }

    public void StopNote(){midiStreamPlayer.MPTK_StopEvent(mptkEvent);}

    public void PlayRune(string rune)
    {
        if (codeCounter >= slots.Length)
            return;
        slots[codeCounter].SendMessage("SetRune", rune);
        codeEntered += rune;
        codeCounter++;
        if(codeCounter == 5)
        {
            CheckPassword();
        }
    }

    public void ClearRunes()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            slots[i].SendMessage("SetRune", "");
        }
        codeEntered = "";
        codeCounter = 0;
    }

    public void CheckPassword()
    {
        if(codeEntered == runePassword)
        {
            Debug.Log("correct password");
            StartCoroutine(RightCode());
        }
        else
        {
            Debug.Log("wrong password");
            StartCoroutine(WrongCode());
        }
        //ClearRunes();
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

    private IEnumerator WrongCode()
    {
        yield return new WaitForSeconds(0.2f);
        //play some incorrect chime
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SendMessage("ColorIndicate", "wrong");
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SendMessage("ColorIndicate", "none");
        }
        ClearRunes();
    }

    private IEnumerator RightCode()
    {
        yield return new WaitForSeconds(0.2f);
        //play some correct chime
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SendMessage("ColorIndicate", "right");
        }
        yield return new WaitForSeconds(2);
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].SendMessage("ColorIndicate", "none");
        }
        ClearRunes();
    }
}

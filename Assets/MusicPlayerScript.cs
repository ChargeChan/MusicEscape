using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayerScript : MonoBehaviour
{
    private AudioSource music;
    public float musicVolume;
    private float loweredVolume;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
        loweredVolume = musicVolume / 4;
        music.volume = musicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Quiet()
    {
        music.volume = loweredVolume;
    }

    public void UnQuiet()
    {
        music.volume = musicVolume;
    }
}

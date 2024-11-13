using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData : MonoBehaviour
{
    public float bpm = 182;//                  17          29  33                              65  69              85
    public string beatSheet = "***************** * * * * * ** ** * * * * * * * * * * * * * * * ** ** * ** ** * ** ** * * * " +
        "* * ** *** *** *** *** ** * * * * * * * ** ** * * * * * * * ** ** * ** ** * * * * * * * ** ** * * " +
        "* ** *** ** * * * * * * * ** ** * ** ** * * * * * * * ** ** * * * * * * * ** ** * ** *** *** *** *" +
        "* * * * * * ** **";
    public AudioClip song;
    public AudioClip perfect;
    public AudioClip good;
    public AudioClip miss;
}

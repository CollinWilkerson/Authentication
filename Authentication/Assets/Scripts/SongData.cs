using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongData : MonoBehaviour
{
    public float bpm = 180;
    public string beatSheet = "***************** * * * * * ** ** * * * * * * * * * * * * * * * ** ** * ** ** * ** ** * * * " +
        "* * ** *** *** *** *** ** * * * * * * * ** ** * * * * * * * ** ** * ** ** * * * * * * * ** ** * * " +
        "* ** *** ** * * * * * * * ** ** * ** ** * * * * * * * ** ** * * * * * * * ** ** * ** *** *** *** *" +
        "* * * * * * ** **";
    public AudioClip song;
    public AudioClip perfect;
    public AudioClip good;
    public AudioClip miss;
}

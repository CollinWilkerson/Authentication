using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmController : MonoBehaviour
{ 
    private AudioSource audioSource;
    private float bpm = 180;
    private int beat = 0;
    private float beatTime = 0;
    private char action = '*';
    private SongData songData;

    private float score = 0;

    private bool isPlaying = false;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        songData = GetComponent<SongData>();
        bpm = songData.bpm;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPlaying)
        {
            return;
        }

        //if the time elapsed is longer than a beat
        if(Time.time - beatTime > 60 / bpm)
        {
            //Debug.Log("Time between beats: " + (Time.time - beatTime));
            //I should modify this to be the nearest beat lest we get off time
            beatTime += 0.33f;
            beat++;
            //Debug.Log("Beat " + beat);
            
        }
        //read a string that contains whether the user should take no action or press a button
        try
        {
            action = songData.beatSheet[beat];
        }
        catch (Exception e){
            //the only exception is index out of bounds which means the song is over
            isPlaying = false;
            beat = 0;
            Leaderboard.instance.SetLeaderboardEntry((int) score);
            Leaderboard.instance.DisplayLeaderboard();
            score = 0;
        }
        Debug.Log("action: " + action);

        //time the players press with the time of the song
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (action == ' ')
            {
                //score is the difference in time from the start of the beat
                float scoreChange = 100 - ((Time.time - beatTime) * 300);
                score += scoreChange;
                Debug.Log("Hit: " + score);
                if(scoreChange > 50)
                {
                    audioSource.PlayOneShot(songData.perfect);
                } 
                else if(scoreChange > 25)
                {
                    audioSource.PlayOneShot(songData.good);
                } 
                else
                {
                    audioSource.PlayOneShot(songData.miss);
                }
            }
            else
            {
                audioSource.PlayOneShot(songData.miss);
            }
        }
        //play an animation based on wether or not the input was good
    }

    //for button
    public void startPlay()
    {
        if (isPlaying)
        {
            return;
        }
        beatTime = Time.time;
        isPlaying = true;
        audioSource.PlayOneShot(songData.song);
    }
}

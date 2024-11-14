using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;
using TMPro;

public class RhythmController : MonoBehaviour
{ 
    private AudioSource audioSource;
    private float bpm = 182;
    private int beat = 0;
    private float beatTime = 0;
    private char action = '*';
    private SongData songData;
    private VideoPlayer videoPlayer;
    public TextMeshProUGUI scoreText;

    private float score = 0;

    private bool isPlaying = false;
    private bool hitIsBuffered = false;

    private void Awake()
    {
        videoPlayer = FindFirstObjectByType<VideoPlayer>();
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
            beatTime += 60 / bpm;
            beat++;
            Debug.Log("adding " + 60/bpm);
            
        }
        //read a string that contains whether the user should take no action or press a button
        try
        {
            action = songData.beatSheet[beat];
        }
        catch (Exception e){
            //the only exception is index out of bounds which means the song is over
            isPlaying = false;
            videoPlayer.Stop();
            beat = 0;
            Leaderboard.instance.SetLeaderboardEntry((int) score);
            Leaderboard.instance.DisplayLeaderboard();
            score = 0;
        }
        Debug.Log("action: " + action);

        //time the players press with the time of the song
        if (Input.GetKeyDown(KeyCode.Space) && !hitIsBuffered) {
            if (action == ' ')
            {
                //score is the difference in time from the start of the beat
                float scoreChange = 100 - ((Time.time - beatTime) * 300);
                score += scoreChange;
                Debug.Log("Hit: " + score);
                scoreText.text = ((int) score).ToString();
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
            else if(songData.beatSheet[beat + 1] == ' ')
            {
                float scoreChange = (Time.time - beatTime) * 300;
                score += scoreChange;
                Debug.Log("Hit: " + score);
                scoreText.text = ((int)score).ToString();
                if (scoreChange > 50)
                {
                    audioSource.PlayOneShot(songData.perfect);
                }
                else if (scoreChange > 25)
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
            StartCoroutine(HitBuffer());
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
        videoPlayer.Play();
        audioSource.PlayOneShot(songData.song);
    }

    private IEnumerator HitBuffer()
    {
        hitIsBuffered = true;
        yield return new WaitForSeconds(0.33f);
        hitIsBuffered = false;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    private Rigidbody rig;

    private float startTime;
    private float timeTaken;

    private int collectablesPicked;
    public int maxCollectables = 10;

    public GameObject playButton;
    public TextMeshProUGUI curTimeText;

    private bool isPlaying;

    private void Awake()
    {
        //FINALLY
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isPlaying)
        {
            return;
        }
        float x = Input.GetAxis("Horizontal") * speed;
        float z = Input.GetAxis("Vertical") * speed;

        rig.velocity = new Vector3(x, rig.velocity.y, z);

        curTimeText.text = (Time.time - startTime).ToString("F2");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collectable"))
        {
            collectablesPicked++;
            //perchance setactive false to make it replayable
            Destroy(other.gameObject);

            if(collectablesPicked == maxCollectables)
            {
                End();
            }
        }
    }

    public void Begin()
    {
        startTime = Time.time;
        isPlaying = true;
        playButton.SetActive(false);
    }

    private void End()
    {
        timeTaken = Time.time - startTime;
        isPlaying = false;
        playButton.SetActive(true);
        Leaderboard.instance.SetLeaderboardEntry(-Mathf.RoundToInt(timeTaken * 1000.0f));
    }
}

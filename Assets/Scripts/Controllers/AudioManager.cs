using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    PlayerController playerController;
    public AudioClip currentClip;

    public AudioClip Danger;
    public AudioClip Sad;
    public AudioClip SadEnding;
    public AudioClip BirthOfAHero;


    bool darkChanged = false;
    bool sadChanged = false;




    public AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (playerController.alliesAlive)
        {
            case 0:
                if (!sadChanged)
                {
                    StopCoroutine("ChangeSongPart1");
                    StopCoroutine("ChangeSongPart2");
                    sadChanged = true;
                    StartCoroutine(ChangeSongPart1(Sad, 3));
                }
                break;
            case 4:
                if (!darkChanged)
                {
                    darkChanged = true;
                    StartCoroutine(ChangeSongPart1(Danger, 3));
                }
                break;
            default:
                break;
        }
    }


    private IEnumerator ChangeSongPart1(AudioClip clip, float lerpRate)
    {

        var timeStep = 0.0f;
        float startPoint = AudioSource.volume;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / lerpRate;
            AudioSource.volume = Mathf.Lerp(startPoint, 0f, timeStep);
            yield return null;
        }

        StartCoroutine(ChangeSongPart2(clip,lerpRate));
    }

    private IEnumerator ChangeSongPart2(AudioClip clip, float lerpRate)
    {
        AudioSource.Stop();
        AudioSource.PlayOneShot(clip);
        var timeStep = 0.0f;
        float startPoint = 0;
        while (timeStep < 1.0f)
        {
            timeStep += Time.deltaTime / lerpRate;
            AudioSource.volume = Mathf.Lerp(startPoint, 0.21f, timeStep);
            if(AudioSource.volume>0.19f)
                AudioSource.volume=0.21f;
            yield return null;
        }
    }


    public void endGameMusic()
    {

        if (playerController.alliesAlive > 4)
        {
            StopCoroutine("ChangeSongPart1");
            StopCoroutine("ChangeSongPart2");
            StartCoroutine(ChangeSongPart1(BirthOfAHero, 2));
        }
        else
        {
            StopCoroutine("ChangeSongPart1");
            StopCoroutine("ChangeSongPart2");
            StartCoroutine(ChangeSongPart1(SadEnding, 2));
        }

       
    }
}

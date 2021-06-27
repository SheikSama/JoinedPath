using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndSceneViewModel : MonoBehaviour
{
    public AudioSource audioSource;
    int currentView = 0;

    public GameObject Slide1;
    public GameObject Slide2;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Insert) && currentView==0)
        {
            audioSource.PlayOneShot(audioSource.clip);
            currentView++;
            NextScreen();
        }

    }


    public void NextScreen()
    {
        if (currentView == 1)
        {
            Slide1.SetActive(false);
            Slide2.SetActive(true);
        } 
    }

    public void EndGame()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Application.Quit();
    }

    public void Restart()
    {
        audioSource.PlayOneShot(audioSource.clip);
        GlobalData.QntRestartGames++;
        SceneManager.LoadScene("StartLevel");
    }

    public void GoToCredits()
    {
        audioSource.PlayOneShot(audioSource.clip);
        SceneManager.LoadScene("Credits");

    }
}

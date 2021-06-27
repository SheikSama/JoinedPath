using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialViewModel : MonoBehaviour
{

    public AudioSource audioSource;
    int currentView = 0;

    public GameObject Slide1;
    public GameObject Slide2;
    public GameObject Slide3;
    public GameObject Slide4;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.Insert))
        {
            currentView++;
            audioSource.PlayOneShot(audioSource.clip);
            NextScreen();
        }

    }


    public void NextScreen() 
    {
        if (currentView == 1)
        {
            Slide1.SetActive(false);
            Slide2.SetActive(true);
            Slide3.SetActive(false);
            Slide4.SetActive(false);
        }
        else if (currentView== 2)
        {
            Slide1.SetActive(false);
            Slide2.SetActive(false);
            Slide3.SetActive(true);
            Slide4.SetActive(false);
        }
        else if (currentView == 3)
        {
            Slide1.SetActive(false);
            Slide2.SetActive(false);
            Slide3.SetActive(false);
            Slide4.SetActive(true);
        }
        else if (currentView == 4)
        {
            SceneManager.LoadScene("StartLevel");
        }
    }

}

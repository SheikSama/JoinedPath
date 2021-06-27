using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndCinematicController : MonoBehaviour
{

    public AudioSource AudioSource;
    public GameObject Player;
    PlayerController playerController;
    public Text QntCompanionsAliveTxt;

    public GameObject text1;
    public GameObject text2;

    int currentText = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerController = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            GlobalData.AlliesAllive = playerController.alliesAlive;
            switch (playerController.alliesAlive)
            {
                case 0:
                    QntCompanionsAliveTxt.text = "You are alone... You left behind all your companions and kept going.\n Now you have an impossible task in front of you...\n Defeating the Big Bad Boss on your own...";
                    break;
                case 1:
                    QntCompanionsAliveTxt.text = "Only one poor soul reached the end of the journey with you... .\n You both are not enough to win, but at least you will not die alone...";
                    break;
                case 2:
                    QntCompanionsAliveTxt.text = "Two of the bravest stand beside you... Don't fear what might happen\n Every single one you left behind was for that moment, and they knew that... do they?";
                    break;
                case 3:
                    QntCompanionsAliveTxt.text = "The last three heroes stand before the Big Bad Boss with you\n The road was hard, but your party might have a change...\n\n But that is a story for another game";
                    break;
                case 4:
                    QntCompanionsAliveTxt.text = "Four heroes stand by your side, they trust the two companions left behind holding the line.\n This is the chance you were building all this journey.";
                    break;
                case 5:
                    QntCompanionsAliveTxt.text = "Five heroes will cry their missing friend.\n But now all of you have one last task to complete... And you will do it together\n\n But that is a story for another game";
                    break;
                case 6:
                    QntCompanionsAliveTxt.text = "All heroes stand before the Big Bad Boss, he trembles in fear.\n It was a long journey, but you made it together your story will be eternal.";
                    break;
                default:
                    break;
            }


            if (Input.GetKeyDown(KeyCode.Joystick1Button1) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Insert) && currentText<=2)
            {
                currentText++;
                NextScreen();
            }



        }
    }

    public void NextScreen()
    {
        AudioSource.PlayOneShot(AudioSource.clip);
        if (currentText == 1)
        {
            text1.SetActive(false);
            text2.SetActive(true);
            
        }
        else if (currentText == 2)
        {
            SceneManager.LoadScene("EndScene");

        }
    }
}

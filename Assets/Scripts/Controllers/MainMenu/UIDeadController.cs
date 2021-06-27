using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIDeadController : MonoBehaviour
{

    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        audioSource.PlayOneShot(audioSource.clip);
        GlobalData.QntRestartGames++;
        SceneManager.LoadScene("StartLevel");
        this.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(audioSource.clip);
        Application.Quit();
    }
}

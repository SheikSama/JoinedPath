using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
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
        SceneManager.LoadScene("Tutorial");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }


    public void QuitGame()
    {
        Application.Quit();
    }

}

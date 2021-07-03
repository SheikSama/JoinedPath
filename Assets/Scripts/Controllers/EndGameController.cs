using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameController : MonoBehaviour
{

    public GameObject AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D entity)
    {
        if (entity.gameObject.tag == "Player")
        {
            entity.GetComponent<PlayerController>().ZeroUntilMidnight = true;
            AudioManager.GetComponent<AudioManager>().endGameMusic();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 cameraPos = this.transform.position;
        if (player.transform.position.y > 0)
        {
            cameraPos.y = player.transform.position.y;
            this.transform.SetPositionAndRotation(cameraPos, this.transform.rotation);
        }
    }
}

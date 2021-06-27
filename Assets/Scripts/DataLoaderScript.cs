using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoaderScript : MonoBehaviour
{

    public Text qntHeroesAlive;
    public Text qntRestarts;
    public GameObject congratsTxt;
    public GameObject TheBestTxt;


    // Start is called before the first frame update
    void Start()
    {
        qntHeroesAlive.text = GlobalData.AlliesAllive.ToString();
        qntRestarts.text = GlobalData.QntRestartGames.ToString();

        if (GlobalData.AlliesAllive == 6)
        {
            congratsTxt.SetActive(true);
            TheBestTxt.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

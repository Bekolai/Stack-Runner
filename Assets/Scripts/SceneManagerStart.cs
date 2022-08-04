using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerStart : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("FirstTime") == 0)
        {
            PlayerPrefs.SetInt("FirstTime", 1);
            PlayerPrefs.SetInt("Level", 1);
        }


        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;


public class GameManager : MonoBehaviour
{

   
    public TMP_Text price,levelText,currencyText,upgradeText;
    public GameObject buyButton, startMenu,finishMenu,charObj;
  


    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "LEVEL "+SceneManager.GetActiveScene().buildIndex.ToString();
        currencyText.text = PlayerPrefs.GetInt("Gold").ToString();
        PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex);
        updateButton(); //updating buy update button

    
       

    }

    public void StartGame()
    {
        startMenu.SetActive(false);
        charObj.GetComponent<MovementController>().StartMovement();
        charObj.GetComponent<PlayerController>().startRunAnim();
        charObj.GetComponent<PlayerController>().setMaxStack((PlayerPrefs.GetInt("Stack")));

    }

    public void NextLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }
    public void BuyStack()
    {
        if (PlayerPrefs.GetInt("Gold")>= ((PlayerPrefs.GetInt("Stack")) + (int)((PlayerPrefs.GetInt("Stack") / 10))))
        {
            PlayerPrefs.SetInt("Gold",PlayerPrefs.GetInt("Gold")-((PlayerPrefs.GetInt("Stack")) + (int)((PlayerPrefs.GetInt("Stack") / 10)))); //update gold
            currencyText.text = (PlayerPrefs.GetInt("Gold").ToString()); //update currency
            PlayerPrefs.SetInt("Stack", PlayerPrefs.GetInt("Stack") + 1); //update stack player pref
            updateButton(); //update button price-interactiblity
            charObj.GetComponent<PlayerController>().setMaxStack((PlayerPrefs.GetInt("Stack"))); //update max stack on player

        }
     
    }
  
    void updateButton()
    {
        if (PlayerPrefs.GetInt("Gold") < ((PlayerPrefs.GetInt("Stack")) + (int)((PlayerPrefs.GetInt("Stack") / 10))))
        {
            buyButton.GetComponent<Button>().interactable = false;
        }
        upgradeText.text = "UPGRADE MAX STACK TO " + ((PlayerPrefs.GetInt("Stack") + 1).ToString());
        price.text = ((PlayerPrefs.GetInt("Stack")) + (int)((PlayerPrefs.GetInt("Stack") / 10))).ToString();

    }
    public void Finish()
    {
        finishMenu.SetActive(true);
        currencyText.text = (PlayerPrefs.GetInt("Gold").ToString()); //update total currency
    }
}

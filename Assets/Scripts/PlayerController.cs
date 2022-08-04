using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Animator _anim;
   public  int collectedCoin,stackMax,currenctStack=0;
    public TMP_Text currencyText,stackText,currencyFinaleText,totalText,currentStackText;
    public Slider slider;
   
    // Start is called before the first frame update
    void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
       setMaxStack((PlayerPrefs.GetInt("Stack"))); //update max start at start
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void startRunAnim()
    {
        _anim.SetTrigger("gameStarted");

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gold"))
        {
            Destroy(other.gameObject);
            collectedCoin++;
            currencyText.text = (PlayerPrefs.GetInt("Gold")+collectedCoin).ToString();
        }
        if(other.CompareTag("Trap"))
        {
            Destroy(other.gameObject);
            if(currenctStack!=0)
            {
                currenctStack--;
                slider.value--;
                currentStackText.text = currenctStack.ToString();
                stackAnimCheck();
            }
            
        }
        if(other.CompareTag("Diamond"))
        {
            Destroy(other.gameObject);
            if (currenctStack<stackMax) //check if stack is full
            {
           
                currenctStack++;
                slider.value++;   
                currentStackText.text = currenctStack.ToString();
                stackAnimCheck();
            }
           
        }
        if(other.CompareTag("Finish"))
        {
            Destroy(other.gameObject);
            stackText.text = currenctStack.ToString();
            currencyFinaleText.text = collectedCoin.ToString();
            totalText.text = (currenctStack * collectedCoin).ToString();
            PlayerPrefs.SetInt("Gold", PlayerPrefs.GetInt("Gold") + (currenctStack * collectedCoin));
            if(SceneManager.sceneCountInBuildSettings-1== SceneManager.GetActiveScene().buildIndex)
            {
                PlayerPrefs.SetInt("Level", 1);
            }
            else
            {
           PlayerPrefs.SetInt("Level", SceneManager.GetActiveScene().buildIndex+1);
            }
           
            StartCoroutine(finishMovement());
         
       

          
        }

    }
    IEnumerator finishMovement()
    {  gameObject.GetComponent<MovementController>().StopMovement();
        gameObject.transform.DOMove(new Vector3(0, gameObject.transform.position.y, gameObject.transform.position.z + 3f), 1f);
       yield return new WaitForSeconds(1f);
        gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.5f);
        slider.transform.parent.gameObject.SetActive(false);
        _anim.SetTrigger("gameFinished");
          GameObject.Find("Main Camera").GetComponent<CameraFollow>().finish = true;
        yield return new WaitForSeconds(1f);
      
        GameObject.Find("GameManager").GetComponent<GameManager>().Finish();
    }
    public void setMaxStack(int stack)
    {
        stackMax = stack;
        slider.maxValue = stack * 2; // doubling the max value because of half circular  slider
    }
    void stackAnimCheck()
    {
        if(currenctStack>0)
        {
            _anim.SetBool("hasMoney", true);
        }    
        else
            _anim.SetBool("hasMoney", false);

    }
}

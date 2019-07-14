using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public GameObject resultO;
    public GameObject resultX;
    public GameObject again;
    public bool answer = false;
    int q = 0;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Judge() //判斷答案
    {
        if (!resultO.activeInHierarchy || !resultX.activeInHierarchy)
        {
            if (answer)
            {
                resultO.SetActive(true);
                GameObject.FindWithTag("GM").GetComponent<GameManager>().SendMessage("CloseUIDelay", 9);
                GameObject.FindWithTag("GM").GetComponent<GameManager>().SendMessage("OpenUIDelay", 11 + q);
                GameObject.FindWithTag("GM").GetComponent<GameManager>().SendMessage("CloseUIDelay", 10 + q);
                q++;
                //GameObject.FindWithTag("SE").SendMessage("PlaySE",1);
            }
            if (!answer)
            {
                resultX.SetActive(true);
                again.SetActive(true);
                //GameObject.FindWithTag("SE").SendMessage("PlaySE",2);
            }
        }
    }
    public void CurrectAnswer()
    {
        answer = true;
    }
    public void ErrorAnswer()
    {
        answer = false;
    }
    public void ResetLevel2()
    {
        resultO.SetActive(false);
        resultX.SetActive(false);
        again.SetActive(false);
        answer = false;
    }

}

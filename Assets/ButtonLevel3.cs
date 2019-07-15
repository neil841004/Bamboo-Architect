using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonLevel3 : MonoBehaviour
{
    public Sprite[] btn_Sprite;
    public Sprite[] question_Sprite;
    public Sprite[] ansBox_Sprite;
    public GameObject question;
    public GameObject ansBox;
    public Sprite[] iconOrigin;
    public GameObject[] icon;
    int iPress = 0; //點了幾次
    int iQuestion = 0; //第幾題


    public void Press()
    {
        iPress++;
        if (iPress < 10)
        {
            if (iPress % 2 == 0)
            {
                this.GetComponent<Image>().sprite = btn_Sprite[0];
                if (iPress != 0)
                {
                    iQuestion++;
                    GameObject.FindWithTag("Painter").GetComponent<Painter>().SendMessage("Clear");
                    GameObject.FindWithTag("Painter").SetActive(false);
                    for (int i = 0; i < 3; i++)
                    {
                        icon[i].GetComponent<Image>().sprite = iconOrigin[i];
                    }
                }
            }
            else if (iPress % 2 == 1)
            {
                this.GetComponent<Image>().sprite = btn_Sprite[1];
            }
            question.GetComponent<SpriteRenderer>().sprite = question_Sprite[iPress];
            ansBox.GetComponent<SpriteRenderer>().sprite = ansBox_Sprite[iQuestion];
        }
        if (iPress >= 10)
        {

            GameObject.FindWithTag("GM").GetComponent<GameManager>().SendMessage("CloseUI", 15);
            GameObject.FindWithTag("GM").GetComponent<GameManager>().SendMessage("CloseUI", 16);
            GameObject.FindWithTag("GM").GetComponent<GameManager>().SendMessage("OpenUI", 17);
            ResetIcon();
        }
    }
    public void ResetIcon()
    {
		
        iPress = 0;
        iQuestion = 0;
        this.GetComponent<Image>().sprite = btn_Sprite[0];
        question.GetComponent<SpriteRenderer>().sprite = question_Sprite[0];
        ansBox.GetComponent<SpriteRenderer>().sprite = ansBox_Sprite[0];
        for (int i = 0; i < 3; i++)
        {
            icon[i].GetComponent<Image>().sprite = iconOrigin[i];
        }

    }
}

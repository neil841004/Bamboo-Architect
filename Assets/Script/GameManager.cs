using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] displayUI;
    public static int level3_2 = 1;
    public Sprite optionIconOrigin;
    public GameObject[] optionIcon;
    public bool answer = false;
    public GameObject homeBtn;
    public bool isHome = false;

    // Use this for initialization
    void Start()
    {
        Input.multiTouchEnabled = false;
        // for (int i = 1; i <= 17; i++)
        // {
        //     displayUI[i].SetActive(false);
        // }
        // displayUI[0].SetActive(true);
        // homeBtn.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OpenUI(int i)
    {
        displayUI[i].SetActive(true);
    }
    public void CloseUI(int i)
    {
        displayUI[i].SetActive(false);
    }
    public void OpenUIDelay(int i) //按鈕延遲
    {
        StartCoroutine(functionName(i, true));
    }
    public void CloseUIDelay(int i)
    {
        StartCoroutine(functionName(i, false));
    }
    IEnumerator functionName(int i, bool active)
    {
        yield return new WaitForSeconds(2f);
        if (!isHome) displayUI[i].SetActive(active);
    }
    public void ResetOptionIcon()
    {
        StartCoroutine(ResetOption());
    }
    IEnumerator ResetOption()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < 9; i++)
        {
            optionIcon[i].GetComponent<SpriteRenderer>().sprite = optionIconOrigin;
        }
    }
    public void backHome()
    {
        if (GameObject.FindWithTag("Painter"))
        {
            GameObject.FindWithTag("Painter").GetComponent<Painter>().SendMessage("Clear");
            GameObject.FindWithTag("Painter").SetActive(false);
        }
        for (int i = 1; i <= 17; i++)
        {
            displayUI[i].SetActive(false);
        }
        displayUI[0].SetActive(true);
        for (int i = 0; i < 9; i++)
        {
            optionIcon[i].GetComponent<SpriteRenderer>().sprite = optionIconOrigin;
        }
        isHome = true;
        StartCoroutine(ResetIsHome());
    }
    IEnumerator ResetIsHome()
    {
        yield return new WaitForSeconds(2.1f);
        isHome = false;
    }
}

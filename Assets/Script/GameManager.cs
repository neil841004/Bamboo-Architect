using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] displayUI;
    public static int level3_2 = 1;

    // Use this for initialization
    void Start()
    {
        Input.multiTouchEnabled = false;
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
        yield return new WaitForSeconds(0.08f);
        displayUI[i].SetActive(active);
    }
}

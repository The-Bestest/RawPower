using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    /*static WinLose _instance;
    public static WinLose Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<WinLose>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("Win Lose");
                    _instance = manager.AddComponent<WinLose>();
                }
            }

            return _instance;
        }
    }*/
    public Text WinLoseText;
    //public RawImage GreyScale;
    //public Button Replay;
    /*
    public void Lose()
    {
        StartCoroutine(Fading("Game Over \n The earth is ruined by pollution"));
    }

    public void Win()
    {
        StartCoroutine(Fading("Game Over \n You supplied the earth with power, without destroying the planet, good job"));
    }
    */
    void Start()
    {
        //WinLoseText = GameObject.Find("WinLose").GetComponent<Text>();
        if(GameManager.Instance.wonOrLost)
        {
            WinLoseText.text = "Game over\nYou won!";
        }
        else
        {
            WinLoseText.text = "Game over\nYou lost!";
        }
    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }
    public void Reload()
    {
        SceneManager.LoadScene(1);
    }
    /*
    IEnumerator Fading(string winOrLose)
    {
        GreyScale.gameObject.SetActive(true);
        WinLoseText.gameObject.SetActive(true);
        WinLoseText.text = winOrLose;
        yield return new WaitForSecondsRealtime(2);
        while(GreyScale.color.a < 0.8f)
        {
            GreyScale.color += new Color(0, 0, 0, 0.8f * Time.deltaTime / 3);
            yield return new WaitForEndOfFrame();
        }
        if(GreyScale.color.a >= 0.8f)
        {
            while(WinLoseText.color.a < 1)
            {
                WinLoseText.color += new Color(0, 0, 0, 1 * Time.deltaTime / 3);
                yield return new WaitForEndOfFrame();
            }
        }
        if (WinLoseText.color.a >= 0.9f)
        {
            Replay.gameObject.SetActive(true);
        }
        yield return null;
    }*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{

    public Text WinLoseText;
    public RawImage GreyScale;
    public Button Replay;

    public void Lose()
    {
        StartCoroutine(Fading("Game Over \n You Lose"));
    }

    public void Win()
    {
        StartCoroutine(Fading("Game Over \n You Win"));
    }

    public void Reload()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator Fading(string winOrLose)
    {
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
    }
}

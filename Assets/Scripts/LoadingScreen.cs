using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen: MonoBehaviour
{

    float Timer = 0.3f;
    float FadeIn = 0;
    float Vent = 1;

    public Text LogoText;
    public SpriteRenderer logoMat;
    //Component Sound;

   /*void Awake()
    {
        GetComponent<AudioSource>().PlayDelayed(2.8f);
    }*/

    void Update()
    {
        if (FadeIn <= 1)
        {
            FadeIn += Timer * Time.deltaTime;
            logoMat.color = new Color(1, 1, 1, FadeIn);
            LogoText.color = new Color(0, 0, 0, FadeIn);
        }
        else if (FadeIn >= 1)
        {
            if (Vent >= 0)
            {
                logoMat.color = new Color(1, 1, 1, Vent);
                LogoText.color = new Color(0, 0, 0, Vent);
                Vent -= Timer * Time.deltaTime;
                if (Vent <= 0)
                {
                    SceneManager.LoadScene(1);
                }
            }
        }
    }
}

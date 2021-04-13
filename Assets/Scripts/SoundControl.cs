using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundControl : MonoBehaviour
{
    public AudioSource BackGround;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            BackGround.Stop();
        }
        else if (!BackGround.isPlaying && SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(1))
        {
            BackGround.Play();
        }
    }
}

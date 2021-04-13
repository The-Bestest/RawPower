using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    void Start()
    {

    }

    public void Menu()
    {
        SceneManager.LoadScene(1);
    }
    public void Reload()
    {
        SceneManager.LoadScene(2);
    }
}

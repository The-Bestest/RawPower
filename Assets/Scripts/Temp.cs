using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Temp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(waiting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator waiting()
    {
        yield return new WaitForSecondsRealtime(4);
        SceneManager.LoadScene(2);
    }
}

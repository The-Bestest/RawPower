using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    static PowerManager _instance;
    public static PowerManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PowerManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("Power Manager");
                    _instance = manager.AddComponent<PowerManager>();
                }
            }

            return _instance;
        }
    }
    Planet planet;
    // Start is called before the first frame update
    void Start()
    {
        planet = SelectionManager.Instance.GetSelectedPlanet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

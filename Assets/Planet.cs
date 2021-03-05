using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private bool _isBuilding;
    [SerializeField]
    //private GameObject actionableModel;
    public bool isBuilding
    {
        get
        {
            return _isBuilding;
        }
        set
        {
            Debug.LogWarning(value);
            _isBuilding = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        isBuilding = true;
        //actionableModel = null;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(isBuilding);
    }

    public void startBuilding(GameObject model)
    {
        //actionableModel = model;
        isBuilding = true;
    }

    public void stopBuilding()
    {
        //actionableModel = null;
        isBuilding = false;
    }

}

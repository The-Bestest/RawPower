using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [NonSerialized]
    public GameObject actionableModel;
    public ArrayList actionables = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        actionableModel = null;
    }
}

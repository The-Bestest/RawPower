using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    static MoneyManager _instance;
    public static MoneyManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<MoneyManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("Money Manager");
                    _instance = manager.AddComponent<MoneyManager>();
                }
            }

            return _instance;
        }
    }
    Planet planet;

    public Text MoneyField;

    public float money = 0;
    public int time = 60;

    public void SetMoney(float income)
    {
        money += income;
    }

    public float GetMoney()
    {
        return money;
    }

    void Start()
    {
        planet = SelectionManager.Instance.GetSelectedPlanet();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject action in planet.actionables)
        {
            if (action.GetComponent<Actionable>().state == Actionable.ActionableState.OK)
            {
                SetMoney((action.GetComponent<Actionable>().pollution * Time.deltaTime / time));
            }
        }
        MoneyField.text = "$ " + (int)money;
    }
}

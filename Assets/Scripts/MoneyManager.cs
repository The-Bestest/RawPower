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
    public Text MoneyField;

    public float money = 0;

    public void SetMoney(float income)
    {
        // Positive to add, Negative to subtract
        money += income;

        UpdateMoney();
    }

    public float GetMoney()
    {
        return money;
    }

    public void UpdateMoney()
    {
        // Update money in UI everytime money is changed
        MoneyField.text = "$ " + (int)money;
    }
}

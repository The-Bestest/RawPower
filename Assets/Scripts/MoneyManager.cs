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

    [SerializeField]
    private float _balance = 0f;
    // We need to do this to be able to serialize field in Unity
    public float balance
    {
        get { return _balance;  }
        private set
        {
            _balance = value;
            UpdateMoneyUI();
        }
    }

    public void Start()
    {
        UpdateMoneyUI();
    }

    /// <summary>
    /// Changes the balance by the amount passed in
    /// </summary>
    /// <param name="amount">Positive to add, Negative to subtract</param>
    public void AddAmount(float amount)
    {
        this.balance += amount;
    }

    public void UpdateMoneyUI()
    {
        // Update money in UI everytime money is changed
        MoneyField.text = "$ " + (int)balance;
    }
}

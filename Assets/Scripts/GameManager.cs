using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("Game Manager");
                    _instance = manager.AddComponent<GameManager>();
                }
            }

            return _instance;
        }
    }
    Planet planet;

    public Text WinText;
    public GameObject WinBackground;
    public float expectedGameTimeInSeconds = 300;
    public float moneyCheckIntervalInSeconds = 5;

    private float startTime;
    private float timeToCheckMoney = 0;

    void Start()
    {
        startTime = Time.time;
        timeToCheckMoney = moneyCheckIntervalInSeconds;
        planet = SelectionManager.Instance.GetSelectedPlanet();
    }

    void Update()
    {
        float multiplier = expectedGameTimeInSeconds/30000;
        float timeFactor = Time.deltaTime * multiplier;
        float elapsedTime = Time.time - startTime;

        PollutionManager.Instance.IncreasePollution(GetCurrentPollutionIncrease() * timeFactor * 0.8f);
        PowerManager.Instance.SetPowerLevel(GetCurrentPowerLevel());
        PowerManager.Instance.SetPowerRequirement(GetCurrentPowerRequirement(elapsedTime));
        MoneyManager.Instance.AddAmount(GetCurrentIncome() * timeFactor);

        if(elapsedTime > 20)
        {
            timeToCheckMoney -= Time.deltaTime;
        }
        if(timeToCheckMoney < 0)
        {
            float powerDifference = PowerManager.Instance.currentPowerLevel - PowerManager.Instance.currentPowerRequirement;
            MoneyManager.Instance.AddAmount(powerDifference/2);

            timeToCheckMoney = moneyCheckIntervalInSeconds;
        }

        WinOrLose();
    }

    private void WinOrLose()
    {
        if (PowerManager.Instance.currentPowerRequirement >= PowerManager.Instance.maxRequirement && PowerManager.Instance.currentPowerLevel >= PowerManager.Instance.maxRequirement)
        {
            WinLose.Instance.Win();
            Debug.Log("WIIN");
        }
        else if(MoneyManager.Instance.balance < 0)
        {
            WinLose.Instance.Lose();
            Debug.Log("NO MOENYS");
        }
        else if(PollutionManager.Instance.getPollutionProgress() >= 100)
        {
            WinLose.Instance.Lose();
            Debug.Log("TOO POLUTION");
        }
    }

    private uint GetCurrentPowerLevel()
    {
        uint result = 0;
        foreach (GameObject actionable in planet.actionables)
        {
            Actionable actionableComponent = actionable.GetComponent<Actionable>();
            if (actionable.GetComponent<Actionable>().state == Actionable.ActionableState.OK)
            {
                int actionablePower = actionableComponent.power;
                result += actionablePower > 0 ? (uint) actionablePower : 0;
            }
        }

        return result;
    }

    private float GetCurrentPowerRequirement(float elapsedTime)
    {
        float x = elapsedTime / expectedGameTimeInSeconds;

        x = x > 1 ? 1 : x;

        // Do NOT edit this, otherwise expectedGameTimeInSeconds is nt going to make any sense
        return (Mathf.Pow(x, 2.3f) + 4 * Mathf.Pow(x, 1.1f) - 2 * x) * PowerManager.Instance.maxRequirement / 3;
    } 

    private uint GetCurrentIncome()
    {
        uint result = 0;
        foreach (GameObject actionable in planet.actionables)
        {
            Actionable actionableComponent = actionable.GetComponent<Actionable>();
            if (actionable.GetComponent<Actionable>().state == Actionable.ActionableState.OK)
            {
                int actionableIncome = actionableComponent.income;
                result += actionableIncome > 0 ? (uint)actionableIncome : 0;
            }
        }

        return result;
    }
    private float GetCurrentPollutionIncrease()
    {
        float result = 0;
        foreach (GameObject actionable in planet.actionables)
        {
            Actionable actionableComponent = actionable.GetComponent<Actionable>();
            if (actionable.GetComponent<Actionable>().state == Actionable.ActionableState.OK)
            {
                result += actionableComponent.pollution;
            }
        }

        return result;
    }

}

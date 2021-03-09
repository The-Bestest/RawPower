using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    MoneyManager money;
    PollutionManager pollution;
    PowerManager power;

    public int time = 60; // Full value of money and pollution is added progressively over this amount of time in seconds
    public float powerReq = 2;

    void Start()
    {
        planet = SelectionManager.Instance.GetSelectedPlanet();
        money = this.GetComponent<MoneyManager>();
        pollution = this.GetComponent<PollutionManager>();
        power = this.GetComponent<PowerManager>();
    }

    void Update()
    {
        power.tempPower = 0;
        foreach (GameObject action in planet.actionables)
        {
            if (action.GetComponent<Actionable>().state == Actionable.ActionableState.OK)
            {
                money.SetMoney((action.GetComponent<Actionable>().income * Time.deltaTime / time));

                pollution.pollution += (action.GetComponent<Actionable>().pollution * Time.deltaTime / time);
                pollution.UpdatePollution();

                power.SetPower(action.GetComponent<Actionable>().power);
                power.UpdatePowerUI();
            }
        }
        if (power.powerReq <= power.maxReq)
        {
            power.UpdateRequirement(Mathf.Pow(powerReq, 2) * Time.deltaTime / time);
        }
    }
}

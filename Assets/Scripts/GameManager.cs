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
    MoneyManager money;
    PollutionManager pollution;
    PowerManager power;

    public Text WinText;
    public GameObject WinBackground;

    public int time = 60; // Full value of money and pollution is added progressively over this amount of time in seconds
    public float powerReq = 2;

    private float atimer = 0;

    void Start()
    {
        planet = SelectionManager.Instance.GetSelectedPlanet();
        money = this.GetComponent<MoneyManager>();
        pollution = this.GetComponent<PollutionManager>();
        power = this.GetComponent<PowerManager>();
    }

    void Update()
    {
        atimer += Time.deltaTime;
        if (atimer > 3 && power.powerReq > 10)
        {
            if(power.power < power.powerReq)
            {
                money.SetMoney(power.power - power.powerReq);

                Debug.Log("money check");
            }
            atimer = 0;
        }
        power.tempPower = 0;
        foreach (GameObject action in planet.actionables)
        {
            if (action.GetComponent<Actionable>().state == Actionable.ActionableState.OK)
            {
                money.SetMoney((action.GetComponent<Actionable>().income * Time.deltaTime / time));

                pollution.pollution += (action.GetComponent<Actionable>().pollution * Time.deltaTime / time);
                pollution.UpdatePollution();

                power.tempPower += action.GetComponent<Actionable>().power;
            }
        }
        power.SetPower(power.tempPower);
        power.UpdatePowerUI();
        if (power.powerReq <= power.maxReq)
        {
            power.UpdateRequirement(Mathf.Pow(powerReq, 2) * Time.deltaTime / (time * 10));
        }
        WinOrLose();
    }

    private void WinOrLose()
    {
        if (power.power >= power.maxReq)
        {
            WinText.text = "You Win!!";
            WinBackground.SetActive(true);
        }
        else if(money.money < 0)
        {
            WinText.text = "You Lose!!";
            WinBackground.SetActive(true);
        }
        else if(pollution.pollution >= pollution.maxPollution)
        {
            WinText.text = "You Lose!!";
            WinBackground.SetActive(true);
        }
    }
}

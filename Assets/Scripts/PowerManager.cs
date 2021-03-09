using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Text PowerPrecent;

    public RawImage MaxPower;
    public RawImage CurrentPower;
    public RawImage PowerRequirement;

    public float power = 0;
    public float tempPower = 0;
    public float powerReq = 0;
    public float maxReq = 500;

    public void SetPower(float powerLevel)
    { 
        tempPower += powerLevel; // Accumulates power from all buildings
        power = tempPower;       // Set power to accumulated, so power isn't added from each building every frame
    }

    public float GetPower()
    {
        return power;
    }

    public void UpdateRequirement(float addedPowerReq)
    {
        powerReq += addedPowerReq;
        PowerRequirement.rectTransform.anchoredPosition = new Vector2(PowerRequirement.rectTransform.anchoredPosition.x, PowerRequirement.rectTransform.anchoredPosition.y + addedPowerReq);
    }

    public void UpdatePowerUI()
    {
        CurrentPower.rectTransform.sizeDelta = new Vector2(MaxPower.rectTransform.sizeDelta.x, power);
        CurrentPower.rectTransform.anchoredPosition = new Vector2(0, power / 2);
        PowerPrecent.text = power + " / " + (int)powerReq;
    }
}

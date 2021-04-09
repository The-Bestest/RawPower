using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
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

    public Text PowerText;

    public RawImage MaxPower;
    public RawImage CurrentPower;
    public RawImage PowerRequirement;

    public float currentPowerLevel { get; private set; } = 0;
    public float currentPowerRequirement { get; private set; } = 0;
    public float maxRequirement { get; private set; } = 500;

    public void SetPowerLevel(float newPowerLevel)
    {
        currentPowerLevel = newPowerLevel;
        UpdatePowerUI();
    }

    public void SetPowerRequirement(float newPowerRequirement)
    {
        currentPowerRequirement = newPowerRequirement;
        UpdatePowerUI();
    }

    public void UpdatePowerUI()
    {
        CurrentPower.rectTransform.sizeDelta = new Vector2(MaxPower.rectTransform.sizeDelta.x, currentPowerLevel);
        CurrentPower.rectTransform.anchoredPosition = new Vector2(0, currentPowerLevel / 2);
        PowerText.text = currentPowerLevel + " / " + (int)currentPowerRequirement;
        PowerRequirement.rectTransform.anchoredPosition = new Vector2(PowerRequirement.rectTransform.anchoredPosition.x, currentPowerRequirement);
    }
}

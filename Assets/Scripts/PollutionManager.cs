using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class PollutionManager : MonoBehaviour
{
    static PollutionManager _instance;
    public static PollutionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PollutionManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("Pollution Manager");
                    _instance = manager.AddComponent<PollutionManager>();
                }
            }

            return _instance;
        }
    }

    public Text PollutionText;
    public RawImage CurrentPollution;
    public RawImage TotalPollution;


    private float pollution = 0;
    public float maxPollution { get; private set; } = 100;

    public void IncreasePollution(float pollutionIncrease)
    {
        pollution += pollutionIncrease;
        UpdatePollutionUI();
    }

    public float getPollutionProgress()
    {
        return (pollution * 100 / maxPollution);
    }

    private void UpdatePollutionUI()
    {
        if (pollution < maxPollution)
        {
            PollutionText.text = (int) getPollutionProgress() + "%";
            CurrentPollution.rectTransform.sizeDelta = new Vector2((TotalPollution.rectTransform.sizeDelta.x / 100) * getPollutionProgress() * 0.95f, TotalPollution.rectTransform.sizeDelta.y);
            CurrentPollution.rectTransform.anchoredPosition = new Vector2(10, 0);
        }
        else
        {
            pollution = 100;
            PollutionText.text = "100%";
        }
    }
}

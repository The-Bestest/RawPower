using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    Planet planet;

    public Text PollutionPrecent;
    public RawImage CurrentPollution;
    public RawImage TotalPollution;

    public float time = 60;

    float pollution = 0;
    public float maxPollution = 100;

    private void Start()
    {
        planet = SelectionManager.Instance.GetSelectedPlanet();
    }
    public void Update()
    {
        foreach(GameObject action in planet.actionables)
        {
            if (action.GetComponent<Actionable>().state == Actionable.ActionableState.OK)
            {
                pollution += (action.GetComponent<Actionable>().pollution * Time.deltaTime / time);
            }
        }
        if (pollution < maxPollution)
        {
            PollutionPrecent.text = (int)(pollution / maxPollution * 100) + "%";
            CurrentPollution.rectTransform.sizeDelta = new Vector2((TotalPollution.rectTransform.sizeDelta.x / 100) * (pollution / maxPollution * 100), TotalPollution.rectTransform.sizeDelta.y);
            CurrentPollution.rectTransform.anchoredPosition = new Vector2((TotalPollution.rectTransform.sizeDelta.x / 100) * (pollution / maxPollution * 100) / 2, 0);
        }
        else
        {
            PollutionPrecent.text = "100%";
            //Add lose
        }
    }
}

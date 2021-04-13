using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingTooltip : MonoBehaviour
{
    public Actionable _building = null;
    public Text tooltipText = null;
    public Actionable building
    {
        get { return _building; }
        set
        {
            _building = value;
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        this.tooltipText.text = makeText();
    }

    private string makeText()
    {
        return string.Format(
            "Price: {0} \nIncome: {1} \nPower: {2} \nRepair: {3} \nDemolish: {4} \nPollution: {5}",
            building.price, building.income, building.power, building.repairPrice, building.demolishPrice, building.pollution);
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

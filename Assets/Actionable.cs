using System;
using UnityEngine;
using UnityEngine.Events;

public class Actionable : MonoBehaviour
{
    public enum ActionableState
    {
        OK,
        Broken
    };

    public Material material;
    public Material disabledMaterial;
    public Material fadeoutMaterial;
    public Material breakdownMaterial;


    public ActionableState state = ActionableState.OK;

    private void Start()
    {
        setDefaultMaterial();
        state = ActionableState.OK;
    }

    void OnMouseDown()
    {
        Planet planet = SelectionManager.Instance.GetSelectedPlanet();

        if (planet.actionableModel == gameObject)
        {
            planet.Build();
        }
        else if (!planet.actionableModel)
        {
            planet.Demolish(gameObject);
        }
    }

    void OnMouseOver()
    {
        Planet planet = SelectionManager.Instance.GetSelectedPlanet();

        if (planet.actionableModel == gameObject)
        {
            planet.Hover();
        }
        else if(!planet.actionableModel)
        {
            setFadeoutMaterial();
        }
    }

    private void OnMouseExit()
    {
        if (state == ActionableState.OK)
        {
            setDefaultMaterial();
        } else
        {
            setBreakdownMaterial();
        }
    }
    public void Breakdown()
    {
        state = ActionableState.Broken;
        setBreakdownMaterial();
    }


    public void setDefaultMaterial()
    {
        setMaterial(material);
    }

    public void setDisabledMaterial()
    {
        setMaterial(disabledMaterial);
    }

    public void setFadeoutMaterial()
    {
        setMaterial(fadeoutMaterial);
    }

    public void setBreakdownMaterial()
    {
        setMaterial(breakdownMaterial);
    }

    private void setMaterial(Material material)
    {
        if (material == null)
        {
            Debug.LogError("No material set");
        }
        else
        {
            GetComponent<Renderer>().material = material;
        }
    }
}

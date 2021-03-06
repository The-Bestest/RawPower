using System;
using UnityEngine;
using UnityEngine.Events;

public class Actionable : MonoBehaviour
{
    public Material material;
    public Material disabledMaterial;
    public Material fadeoutMaterial;

    private void Start()
    {
        setDefaultMaterial();
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
        setDefaultMaterial();
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

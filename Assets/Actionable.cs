using System;
using UnityEngine;
using UnityEngine.Events;

public class Actionable : MonoBehaviour
{
    public Material material;
    public Material disabledMaterial;

    void OnMouseDown()
    {
        SelectionManager.Instance.GetSelectedPlanet().Build();
    }

    void OnMouseOver()
    {
        SelectionManager.Instance.GetSelectedPlanet().Hover();
    }

    public void setDefaultMaterial()
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

    public void setDisabledMaterial()
    {
        if (disabledMaterial == null)
        {
            Debug.LogError("No material set");
        }
        else
        {
            GetComponent<Renderer>().material = disabledMaterial;
        }
    }
}

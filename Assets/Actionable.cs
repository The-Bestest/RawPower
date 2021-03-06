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

    void Update()
    {
        if(isActionableHit())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Planet planet = SelectionManager.Instance.GetSelectedPlanet();

                if (planet.actionableModel == gameObject)
                {
                    planet.Build();
                }

                if(state == ActionableState.Broken)
                {
                    Repair();
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Planet planet = SelectionManager.Instance.GetSelectedPlanet();

                if (!planet.actionableModel)
                {
                    planet.Demolish(gameObject);
                }
            }
        }
    }

    void OnMouseDown()
    {

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
    public void Repair()
    {
        state = ActionableState.OK;
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

    private bool isActionableHit()
    {
        return GetClickedGameObject() == gameObject;
    }
    private static GameObject GetClickedGameObject()
    {
        // Builds a ray from camera point of view to the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Casts the ray and get the first game object hit
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            return hit.transform.gameObject;
        else
            return null;
    }

}

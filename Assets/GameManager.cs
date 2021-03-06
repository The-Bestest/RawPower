using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Planet planetPrefab;
    private static Planet _planet;
    public Planet planet
    {
        get { return _planet; }

        private set { _planet = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(!planet)
        {
            planet = Instantiate(planetPrefab);
        }
    }
    void Update()
    {
        ShowActionableModel();
    }


    public void startBuilding(GameObject model)
    {
        if (planet.actionableModel)
        {
            Destroy(planet.actionableModel);
        }

        planet.actionableModel = Instantiate(model);
    }

    public void stopBuilding()
    {
        Debug.Log(planet.actionableModel);

        Destroy(planet.actionableModel);
        planet.actionableModel = null;
    }

    public void Build()
    {
        if (!canBuild(planet.actionableModel))
        {
            return;
        }

        planet.actionables.Add(Instantiate(planet.actionableModel));

        this.stopBuilding();
    }

    public void Hover()
    {
        if (!planet.actionableModel)
        {
            return;
        }

        if (canBuild(planet.actionableModel))
        {
            planet.actionableModel.GetComponent<Actionable>().setDefaultMaterial();
        }
        else
        {
            planet.actionableModel.GetComponent<Actionable>().setDisabledMaterial();
        }
    }

    private void ShowActionableModel()
    {
        if (!planet.actionableModel)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 sphereCenter = planet.transform.position;
        float sphereRadius = planet.GetComponent<SphereCollider>().radius;
        float distance;

        if (DoesRayIntersectSphere(ray, sphereCenter, sphereRadius, out distance))
        {
            planet.actionableModel.SetActive(true);

            Vector3 position = ray.GetPoint(distance);
            Vector3 normal = (position - sphereCenter).normalized;
            Quaternion rotation = Quaternion.LookRotation(normal);

            planet.actionableModel.transform.position = position;
            planet.actionableModel.transform.rotation = rotation;
        }
        else
        {
            planet.actionableModel.SetActive(false);
        }
    }
    bool canBuild(GameObject objectToBuild)
    {
        if (!objectToBuild)
        {
            return false;
        }

        if (planet.actionables.Contains(objectToBuild))
        {
            Debug.LogError("Object already built");
            return false;
        }

        Collider newCollider = objectToBuild.GetComponent<Collider>();

        foreach (GameObject builtObject in planet.actionables)
        {
            Collider oldCollider = builtObject.GetComponent<Collider>();
            if (newCollider.bounds.Intersects(oldCollider.bounds))
            {
                return false;
            }
        }

        return true;
    }

    private bool DoesRayIntersectSphere(Ray ray, Vector3 sphereOrigin, float sphereRadius, out float distance)
    {
        Vector3 localPoint = ray.origin - sphereOrigin;
        float temp = -Vector3.Dot(localPoint, ray.direction);
        float det = temp * temp - Vector3.Dot(localPoint, localPoint) + sphereRadius * sphereRadius;
        if (det < 0) { distance = Mathf.Infinity; return false; }
        det = Mathf.Sqrt(det);
        float intersection0 = temp - det;
        float intersection1 = temp + det;
        if (intersection0 >= 0)
        {
            if (intersection1 >= 0)
            {
                distance = Mathf.Min(intersection0, intersection1); return true;
            }
            else
            {
                distance = intersection0; return true;
            }
        }
        else if (intersection1 >= 0)
        {
            distance = intersection1; return true;
        }
        else
        {
            distance = Mathf.Infinity; return false;
        }
    }

}

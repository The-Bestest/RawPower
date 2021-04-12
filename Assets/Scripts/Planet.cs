using System;
using System.Collections;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [NonSerialized]
    private GameObject _actionableModel;
    public GameObject actionableModel
    {
        get { return _actionableModel;  }
        private set { _actionableModel = value; }
    }
    public ArrayList actionables = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        actionableModel = null;
    }

    void Update()
    {
        ShowActionableModel();
    }

    public void startBuilding(GameObject model)
    {
        if (actionableModel) // If we are already building something
        {
            stopBuilding();
        }

        actionableModel = Instantiate(model);
    }

    public bool isBuilding()
    {
        return actionableModel != null;
    }

    public void stopBuilding()
    {
        Destroy(actionableModel);
        actionableModel = null;
    }

    public void Build()
    {
        if (!canBuild(actionableModel))
        {
            return;
        }

        int price = actionableModel.GetComponent<Actionable>().price;
        MoneyManager.Instance.AddAmount(-price);

        actionables.Add(Instantiate(actionableModel));

        stopBuilding();
    }

    public void Demolish(GameObject actionable)
    {
        int price = actionable.GetComponent<Actionable>().demolishPrice;
        if (price <= MoneyManager.Instance.balance)
        {
            actionables.Remove(actionable);
            Destroy(actionable);
            MoneyManager.Instance.AddAmount(-price);
        }
    }

    public void Hover()
    {
        if (!actionableModel)
        {
            return;
        }

        if (canBuild(actionableModel))
        {
            actionableModel.GetComponent<Actionable>().setDefaultMaterial();
        }
        else
        {
            actionableModel.GetComponent<Actionable>().setDisabledMaterial();
        }
    }

    private void ShowActionableModel()
    {
        if (!actionableModel)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 sphereCenter = transform.position;
        float sphereRadius = GetComponent<SphereCollider>().radius;
        float distance;

        if (DoesRayIntersectSphere(ray, sphereCenter, sphereRadius, out distance))
        {
            actionableModel.SetActive(true);

            Vector3 position = ray.GetPoint(distance);
            Vector3 normal = (position - sphereCenter).normalized;
            Quaternion rotation = Quaternion.LookRotation(normal);

            actionableModel.transform.position = position;
            actionableModel.transform.rotation = rotation;
        }
        else
        {
            actionableModel.SetActive(false);
        }
    }
    bool canBuild(GameObject objectToBuild)
    {
        if (!objectToBuild)
        {
            return false;
        }

        if (actionables.Contains(objectToBuild))
        {
            Debug.LogError("Object already built");
            return false;
        }

        int price = objectToBuild.GetComponent<Actionable>().price;
        if (MoneyManager.Instance.balance < price)
        {
            return false;
        }

        Collider newCollider = objectToBuild.GetComponent<Collider>();

        foreach (GameObject builtObject in actionables)
        {
            Collider oldCollider = builtObject.GetComponent<Collider>();
            if (newCollider.bounds.Intersects(oldCollider.bounds))
            {
                return false;
            }
        }

        return true;
    }

    private static bool DoesRayIntersectSphere(Ray ray, Vector3 sphereOrigin, float sphereRadius, out float distance)
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

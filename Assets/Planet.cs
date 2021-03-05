using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField]
    private GameObject _actionableModel;
    public GameObject actionableModel
    {
        get
        {
            return _actionableModel;
        }
        set
        {
            Debug.LogWarning(value);
            _actionableModel = value;
        }
    }

    private ArrayList actionables = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        actionableModel = null;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(actionableModel);
        ShowActionableModel();
    }

    private void ShowActionableModel()
    {
        if (!actionableModel)
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 sphereCenter = this.transform.position;
        float sphereRadius = this.GetComponent<SphereCollider>().radius;
        float distance;

        bool isRayThroughSphere = CheckRaySphere(ray, sphereCenter, sphereRadius, out distance);
        Vector3 position = ray.GetPoint(distance);

        //Debug.Log(position);
        if (isRayThroughSphere)
        {
            actionableModel.SetActive(true);
            // did hit sphere.
            //Vector3 position = ray.GetPoint(distance);
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

    public void startBuilding(GameObject model)
    {
        actionableModel = Instantiate(model);
    }

    public void stopBuilding()
    {
        Destroy(actionableModel);
        actionableModel = null;
    }

    public void Hover(GameObject objectHovered)
    {
        if (actionables.Contains(objectHovered))
        {
            return;
        }

        if (canBuild(objectHovered))
        {
            objectHovered.GetComponent<Actionable>().setDefaultMaterial();
        }
        else
        {
            objectHovered.GetComponent<Actionable>().setDisabledMaterial();
        }
    }

    // no hoverModel here for some reason
    public void Build(GameObject objectToBuild)
    {
        if (!canBuild(objectToBuild))
        {
            return;
        }

        actionables.Add(Instantiate(objectToBuild));

        this.stopBuilding();
    }

    bool canBuild(GameObject objectToBuild)
    {
        if (actionables.Contains(objectToBuild))
        {
            Debug.LogError("Object already built");
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

    private bool CheckRaySphere(Ray ray, Vector3 sphereOrigin, float sphereRadius, out float distance)
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

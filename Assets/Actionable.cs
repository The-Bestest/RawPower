using System;
using UnityEngine;
using UnityEngine.Events;

public class Actionable : MonoBehaviour
{

    [Serializable]
    public class OnClick : UnityEvent<GameObject> { }
    [Serializable]
    public class OnHover : UnityEvent<GameObject> { }

    public Material material;
    public Material disabledMaterial;

    public OnClick onClick = new OnClick();
    public OnHover onHover = new OnHover();
    // Start is called before the first frame update
    void Start()
    {
    }

    void OnMouseDown()
    {
        onClick.Invoke(gameObject);
    }

    void OnMouseOver()
    {
        onHover.Invoke(gameObject);
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

    // Update is called once per frame
    void Update()
    {

    }
}

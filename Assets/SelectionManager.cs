using UnityEngine;

// see https://gamedev.stackexchange.com/a/189657/64597
public class SelectionManager : MonoBehaviour
{

    // Quick & dirty singleton pattern with lazy instantiation,
    // so it works even if you don't have an instance in your scene.
    static SelectionManager _instance;
    public static SelectionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SelectionManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("Selection Manager");
                    _instance = manager.AddComponent<SelectionManager>();
                }
            }

            return _instance;
        }
    }

    Planet _selectedPlanet;

    void Awake()
    {
        // Assign a default selection.
        // Replace this with your actual selection logic when the time comes.
        _selectedPlanet = FindObjectOfType<Planet>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetClickedGameObject() == null)
            {
                _selectedPlanet.stopBuilding();
            }
        }
    }

    public Planet GetSelectedPlanet() { return _selectedPlanet; }


    public void startBuildingOnCurrentPlanet(GameObject actionable)
    {
        _selectedPlanet.startBuilding(actionable);
    }
    public void stopBuildingOnCurrentPlanet()
    {
        _selectedPlanet.stopBuilding();
    }

    public void SetSelectedPlanet(Planet planet)
    {
        // TODO: you might want to call on OnSelect / OnDeselect here,
        // or abort up any UI operations currently in progress on the selected planet.
        _selectedPlanet = planet;
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
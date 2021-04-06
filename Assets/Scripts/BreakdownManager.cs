using UnityEngine;
using UnityEngine.EventSystems;

public class BreakdownManager : MonoBehaviour
{
    static BreakdownManager _instance;
    public static BreakdownManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<BreakdownManager>();
                if (_instance == null)
                {
                    GameObject manager = new GameObject("Breakdown Manager");
                    _instance = manager.AddComponent<BreakdownManager>();
                }
            }

            return _instance;
        }
    }

    [Range(0.0f, 1.0f)]
    public float breakdownChance;

    void Update()
    {
        float eventSize = Random.Range(0, 1.0f);
        Planet planet = SelectionManager.Instance.GetSelectedPlanet();

        if(planet.actionables.Count <= 0)
        {
            return;
        }

        if (eventSize < (breakdownChance / 8000 * planet.actionables.Count))
        {
            int randomIndex = Random.Range(0, planet.actionables.Count);
            GameObject randomObject = planet.actionables[randomIndex] as GameObject;

            randomObject.GetComponent<Actionable>().Breakdown();
        }
    }
}

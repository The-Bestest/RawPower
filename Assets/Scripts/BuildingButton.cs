using UnityEngine;
using UnityEngine.EventSystems;

public class BuildingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BuildingTooltip tooltip;
    public Actionable building;
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.building = building;
        tooltip.gameObject.SetActive(true);
        tooltip.transform.localPosition = new Vector2(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.gameObject.SetActive(false);
    }
}

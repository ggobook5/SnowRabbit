using UnityEngine;
using UnityEngine.EventSystems;

public class MenuItem : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    public int index;
    public PauseManager pauseManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        pauseManager.SetSelectionByPointer(index);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        pauseManager.SetSelectionByPointer(index);
        pauseManager.ClickCurrentSelection();
    }
}
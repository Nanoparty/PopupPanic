using UnityEngine.EventSystems;
using UnityEngine;

public class Clickable : MonoBehaviour, IPointerDownHandler
{
    bool clicked = false;
    public void OnPointerDown(PointerEventData eventData)
    {
        clicked = true;
    }

    public bool getClicked()
    {
        return clicked;
    }
    public void setClicked(bool b)
    {
        clicked = b;
    }

    private void Update()
    {
        if (clicked)
        {
            Debug.Log("ERROR CLICK");
            SoundManager.sm.PlayErrorSound();
            clicked = false;
        }
    }
}
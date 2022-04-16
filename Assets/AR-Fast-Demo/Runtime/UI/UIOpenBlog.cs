using UnityEngine;
using UnityEngine.EventSystems;

public class UIOpenBlog : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Application.OpenURL("https://t.me/dyadichenkoga");
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButton : EventTrigger
{
    private bool _inAir = false;
    private Transform _content;

    public bool HasItem = false;

    private void Start()
    {
        _content = transform.Find("ContentParent");
    }

    private void Update()
    {
        if(!_inAir)
        {
            return;
        }

        _content.position = Input.mousePosition;
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if(!HasItem)
        {
            return;
        }

        switch (eventData.button)
        {
            case PointerEventData.InputButton.Left:
                Debug.Log("Left click");
                _inAir = true;
                break;
            case PointerEventData.InputButton.Right:
                Debug.Log("Right click");
                break;
            case PointerEventData.InputButton.Middle:
                Debug.Log("Middle click");
                break;
        }
    }
}

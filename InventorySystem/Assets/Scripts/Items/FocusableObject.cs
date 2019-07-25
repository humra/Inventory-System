using UnityEngine;

public class FocusableObject : MonoBehaviour
{
    [SerializeField]
    private float focusDuration = 3;

    public IFocusableObjectHandler focusableObjectHandler;

    private void OnMouseDown()
    {
        focusableObjectHandler.SetFocus(this.transform, focusDuration);
    }
}

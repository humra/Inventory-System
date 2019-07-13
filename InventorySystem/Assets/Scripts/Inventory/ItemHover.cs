using UnityEngine;
using UnityEngine.UI;

public class ItemHover : MonoBehaviour
{
    public Image _image;
    public Text _text;

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}

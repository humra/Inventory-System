using UnityEngine;
using UnityEngine.UI;

public class ItemHover : MonoBehaviour
{
    public Image _image;
    public Text _text;

    private void Start()
    {
        //_image = GetComponentInChildren<Image>();
        //_text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}

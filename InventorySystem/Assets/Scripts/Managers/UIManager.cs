using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _inventoryPanel;
    private GameObject _equipmentPanel;
    private GameObject _inventoryButton;
    private GameObject _equipmentButton;

    private void Start()
    {
        _inventoryPanel = GameObject.FindGameObjectWithTag("InventoryPanel");
        _inventoryButton = GameObject.FindGameObjectWithTag("InventoryBtn");
        _equipmentPanel = GameObject.FindGameObjectWithTag("EquipmentPanel");
        _equipmentButton = GameObject.FindGameObjectWithTag("EquipmentBtn");

        _inventoryPanel.SetActive(false);
        _equipmentPanel.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            ToggleInventory();
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            ToggleEquipment();
        }
    }

    public void ToggleInventory()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        _inventoryButton.SetActive(!_inventoryButton.activeSelf);
    }

    public void ToggleEquipment()
    {
        _equipmentPanel.SetActive(!_equipmentPanel.activeSelf);
        _equipmentButton.SetActive(!_equipmentButton.activeSelf);
    }
}

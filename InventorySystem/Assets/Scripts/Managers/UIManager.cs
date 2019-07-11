﻿using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private GameObject _inventoryPanel;
    private GameObject _equipmentPanel;
    private GameObject _attributesPanel;
    private GameObject _inventoryButton;
    private GameObject _equipmentButton;
    private GameObject _attributesButton;

    private Text _str;
    private Text _dex;
    private Text _con;
    private Text _int;
    private Text _wis;
    private Text _cha;

    private void Start()
    {
        _inventoryPanel = GameObject.FindGameObjectWithTag("InventoryPanel");
        _inventoryButton = GameObject.FindGameObjectWithTag("InventoryBtn");
        _equipmentPanel = GameObject.FindGameObjectWithTag("EquipmentPanel");
        _equipmentButton = GameObject.FindGameObjectWithTag("EquipmentBtn");
        _attributesPanel = GameObject.FindGameObjectWithTag("AttributesPanel");
        _attributesButton = GameObject.FindGameObjectWithTag("AttributesBtn");

        _str = GameObject.FindGameObjectWithTag("StrValue").GetComponent<Text>();
        _dex = GameObject.FindGameObjectWithTag("DexValue").GetComponent<Text>();
        _con = GameObject.FindGameObjectWithTag("ConValue").GetComponent<Text>();
        _int = GameObject.FindGameObjectWithTag("IntValue").GetComponent<Text>();
        _wis = GameObject.FindGameObjectWithTag("WisValue").GetComponent<Text>();
        _cha = GameObject.FindGameObjectWithTag("ChaValue").GetComponent<Text>();

        _updateAttributeValues();

        _inventoryPanel.SetActive(false);
        _equipmentPanel.SetActive(false);
        _attributesPanel.SetActive(false);
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
        if(Input.GetKeyDown(KeyCode.C))
        {
            ToggleAttributes();
        }
    }

    private void _updateAttributeValues()
    {
        _str.text = PlayerAttributes.Strength.ToString();
        _dex.text = PlayerAttributes.Dexterity.ToString();
        _con.text = PlayerAttributes.Constitution.ToString();
        _int.text = PlayerAttributes.Intelligence.ToString();
        _wis.text = PlayerAttributes.Wisdom.ToString();
        _cha.text = PlayerAttributes.Charisma.ToString();
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

    public void ToggleAttributes()
    {
        _attributesPanel.SetActive(!_attributesPanel.activeSelf);
        _attributesButton.SetActive(!_attributesButton.activeSelf);
    }
}

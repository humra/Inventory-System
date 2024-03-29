﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
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
    private Text _luc;

    private Text _hoverText;
    private Text _infoText;

    private void Start()
    {
        _inventoryPanel = GameObject.Find("Inventory");
        _inventoryButton = GameObject.Find("InventoryBtn");
        _equipmentPanel = GameObject.Find("Equipment");
        _equipmentButton = GameObject.Find("EquipmentBtn");
        _attributesPanel = GameObject.Find("Attributes");
        _attributesButton = GameObject.Find("AttributesBtn");

        _str = GameObject.Find("StrValueTxt").GetComponent<Text>();
        _dex = GameObject.Find("DexValueTxt").GetComponent<Text>();
        _con = GameObject.Find("ConValueTxt").GetComponent<Text>();
        _int = GameObject.Find("IntValueTxt").GetComponent<Text>();
        _wis = GameObject.Find("WisValueTxt").GetComponent<Text>();
        _cha = GameObject.Find("ChaValueTxt").GetComponent<Text>();
        _luc = GameObject.Find("LucValueTxt").GetComponent<Text>();

        _updateAttributeValues();

        _hoverText = GameObject.Find("HoverText").GetComponent<Text>();
        _infoText = GameObject.Find("InfoText").GetComponent<Text>();

        _inventoryPanel.SetActive(false);
        _equipmentPanel.SetActive(false);
        _attributesPanel.SetActive(false);
        _hoverText.gameObject.SetActive(false);
        _infoText.text = "";
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

        if(_hoverText.gameObject.activeSelf)
        {
            _hoverText.transform.position = Input.mousePosition;
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
        _luc.text = PlayerAttributes.Luck.ToString();
    }

    public void ToggleInventory()
    {
        _inventoryPanel.SetActive(!_inventoryPanel.activeSelf);
        _inventoryButton.SetActive(!_inventoryButton.activeSelf);

        bool opened = _inventoryPanel.activeSelf;

        Analytics.CustomEvent("UI window interaction", new Dictionary<string, object>
        {
            { "Inventory",  opened.ToString()}
        });

        if (!_inventoryPanel.activeSelf && Inventory.Instance.TemporaryItemExists()) {
            Inventory.Instance.CancelItemSwap();
        }
    }

    public void ToggleEquipment()
    {
        _equipmentPanel.SetActive(!_equipmentPanel.activeSelf);
        _equipmentButton.SetActive(!_equipmentButton.activeSelf);

        bool opened = _equipmentPanel.activeSelf;

        Analytics.CustomEvent("UI window interaction", new Dictionary<string, object>
        {
            { "Equipment",  opened.ToString()}
        });
    }

    public void ToggleAttributes()
    {
        _attributesPanel.SetActive(!_attributesPanel.activeSelf);
        _attributesButton.SetActive(!_attributesButton.activeSelf);

        bool opened = _attributesPanel.activeSelf;

        Analytics.CustomEvent("UI window interaction", new Dictionary<string, object>
        {
            { "Attributes",  opened.ToString()}
        });
    }

    public void UpdateAttributes()
    {
        _updateAttributeValues();
    }

    public void ShowHoverText(string itemName)
    {
        _hoverText.gameObject.SetActive(true);
        _hoverText.text = itemName + "\n";
    }

    public void ShowHoverText(string itemName, int maxDurability, int currentDurability)
    {
        _hoverText.gameObject.SetActive(true);
        _hoverText.text = itemName + "\n" + currentDurability + "/" + maxDurability + "\n";
    }

    public void HideHoverText()
    {
        _hoverText.gameObject.SetActive(false);
    }

    public void ShowInfoMessage(string message)
    {
        _infoText.text = message;
    }
}

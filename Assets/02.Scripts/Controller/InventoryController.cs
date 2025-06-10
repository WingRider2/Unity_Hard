using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    public Transform Inventory;
    public Transform Status;

    public TextMeshProUGUI TitleText;
    public Button SwitchButton;
    public Button UpgradeButton;

    public ItemSlot[] slots;
    public Transform slotPanel;

    DataManager dataManager;

    public ProjectileData ProjectileData;
    public Action OnUpgrade;
    private void Start()
    {
        dataManager = DataManager.Instance;

        slots = new ItemSlot[slotPanel.childCount];

        for (int i = 0; i < dataManager.ProjectileDataKeys.Count; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = dataManager.ProjectileDataKeys[i];
            slots[i].inventory = this;
            slots[i].Set(dataManager.getData(dataManager.ProjectileDataKeys[i]));
        }

        UpgradeButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.AddListener(onUpgradeBtn);
    }

    public void onUpgradeBtn()
    {
        ProjectileData.Upgrade();
        OnUpgrade?.Invoke();
    }
}

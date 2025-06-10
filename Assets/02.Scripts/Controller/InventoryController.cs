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
    public Button SwitchButton;
    public bool isOnInventory = true;
    public TextMeshProUGUI TitleText;

    public Button UpgradeButton;

    public ItemSlot[] slots;
    public Transform slotPanel;

    DataManager dataManager;

    public ProjectileData ProjectileData;
    public Action OnUpgrade;
    public TextMeshProUGUI UpgradePrice;
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

        SwitchButton.onClick.RemoveAllListeners();
        SwitchButton.onClick.AddListener(toggle);

        UpgradeButton.onClick.RemoveAllListeners();
        UpgradeButton.onClick.AddListener(onUpgradeBtn);
    }

    public void onUpgradeBtn()
    {
        if (PlayerManager.Instance.runtimeStatus.Gold >= ProjectileData.UpgradePrice)
        {
            ProjectileData.Upgrade();
            OnUpgrade?.Invoke();

            PlayerManager.Instance.runtimeStatus.ChangedGold(-ProjectileData.UpgradePrice);
        }
        else
        {
            Debug.Log("돈없다");
        }

    }
    
    public void toggle()
    {
        Inventory.gameObject.SetActive(!isOnInventory);
        Status.gameObject.SetActive(isOnInventory);
        TitleText.text = (isOnInventory ? "인벤토리" : "상태");
        isOnInventory = !isOnInventory;
    }
}

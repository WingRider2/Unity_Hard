using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{
    public ProjectileData item;
    
    public InventoryController inventory;
    public Button button;
    public Image icon;
    public TextMeshProUGUI quatityText;

    public int index;

    private void Start()
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickButton);


    }
    public void Set(ProjectileData ProjectileData)
    {
        icon.gameObject.SetActive(true);
        item = ProjectileData;
        icon.sprite = item.icon;
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        quatityText.text = string.Empty;
    }

    public void Upgrade()
    {
        quatityText.text = inventory.ProjectileData.UpgradeLevel.ToString();
    }
    public void OnClickButton()
    {        
        inventory.ProjectileData = item;
        PlayerManager.Instance.controller.poolType = item.poolType;

        inventory.OnUpgrade = null;
        inventory.OnUpgrade += Upgrade;

        inventory.UpgradePrice.text = item.UpgradePrice.ToString();
    }
}

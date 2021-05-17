using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class InventoryUIDetails : MonoBehaviour {
    Item item;
    Button selectedItemButton, itemInteractButton;
    Text itemNameText, itemDescriptionText, itemInteractButtonText;

    public Text statText;
    void Start()
    {
        itemNameText = transform.Find("Item_Name").GetComponent<Text>();
        statText = transform.Find("Stat_List").GetComponent<Text>();
        itemDescriptionText = transform.Find("Item_Description").GetComponent<Text>();
        itemInteractButton = transform.GetComponentInChildren<Button>();
        itemInteractButtonText = itemInteractButton.GetComponentInChildren<Text>();
        gameObject.SetActive(false);
    }

    public void SetItem(Item item, Button selectedButton)
    {
        gameObject.SetActive(true);
        statText.text = "";
        if (item.Stats != null)
        {
            foreach(BaseStat stat in item.Stats)
            {
                statText.text += stat.StatName + ": " + stat.BaseValue + "\n";
            }
        }
        itemInteractButton.onClick.RemoveAllListeners();
        this.item = item;
        selectedItemButton = selectedButton;
        itemNameText.text = item.ItemName;
        itemDescriptionText.text = item.Description;
        itemInteractButtonText.text = item.ActionName;
        itemInteractButton.onClick.AddListener(OnItemInteract);
    }

    public void OnItemInteract()
    {
        if (item.ItemType == Item.ItemTypes.Consumable)
        {
            InventoryController.Instance.ConsumeItem(item);
            FindObjectOfType<Player>().currentHealth += 50;
            Destroy(selectedItemButton.gameObject);
        }
        else if (item.ItemType == Item.ItemTypes.Weapon)
        {
            InventoryController.Instance.EquipItem(item);
            Destroy(selectedItemButton.gameObject);
        }
        item = null;
        gameObject.SetActive(false);
    }
}

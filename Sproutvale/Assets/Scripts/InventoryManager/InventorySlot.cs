using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public Text amountText;
    public Button deleteButton;

    ItemData item;

    public void SetSlot(ItemData newItem, int amount)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        amountText.text = amount.ToString();
        amountText.gameObject.SetActive(true);

        deleteButton.onClick.RemoveAllListeners();
        deleteButton.onClick.AddListener(RemoveOne);
        deleteButton.gameObject.SetActive(true);
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        amountText.text = "";
        amountText.gameObject.SetActive(false);

        deleteButton.gameObject.SetActive(false);
    }

    void RemoveOne()
    {
        Inventory.Instance.RemoveItem(item, 1);
    }
}

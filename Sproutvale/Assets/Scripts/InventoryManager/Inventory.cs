using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance;

    [System.Serializable]
    public class InventoryItem
    {
        public ItemData item;
        public int amount;
    }

    public List<InventoryItem> items = new List<InventoryItem>();

    [Header("UI Slots")]
    public InventorySlot[] slots;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ================= ADD ITEM =================
    public void AddItem(ItemData item, int amount)
    {
        foreach (var invItem in items)
        {
            if (invItem.item == item)
            {
                invItem.amount += amount;
                UpdateUI();
                return;
            }
        }

        InventoryItem newItem = new InventoryItem();
        newItem.item = item;
        newItem.amount = amount;
        items.Add(newItem);

        UpdateUI();
    }

    // ================= REMOVE ITEM =================
    public void RemoveItem(ItemData item, int amount)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
            {
                items[i].amount -= amount;

                if (items[i].amount <= 0)
                    items.RemoveAt(i);

                UpdateUI();
                return;
            }
        }
    }

    // ================= UPDATE UI =================
    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
                slots[i].SetSlot(items[i].item, items[i].amount);
            else
                slots[i].ClearSlot();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    public float price = 1;
}

public class ShopScrollList : MonoBehaviour
{
    public float gold = 0.0f;
    public List<Item> itemList;
    public ShopScrollList otherShop;
    public SimpleObjectPool buttonObjectPool;
    [HideInInspector]
    public string currentItemSelected;
    public TextMeshProUGUI myGoldDisplay;
    public Transform contentPanel;
    public UserStatistics userStatistics;

    void Start()
    {
        RefreshDisplay();
    }

    void RefreshDisplay()
    {
        myGoldDisplay.text = "Gold: " + gold.ToString();
        RemoveButtons();
        AddButtons();
    }

    private void RemoveButtons()
    {
        while (contentPanel.childCount > 0)
        {
            GameObject toRemove = transform.GetChild(0).gameObject;
            buttonObjectPool.ReturnObject(toRemove);
        }
    }

    private void AddButtons()
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];
            GameObject newButton = buttonObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            SampleButton sampleButton = newButton.GetComponent<SampleButton>();
            sampleButton.Setup(item, this);
        }
    }

    public void TryTransferItemToOtherShop(Item item)
    {
        if (otherShop.gold >= item.price)
        {
            gold += item.price;
            otherShop.gold -= item.price;

            if (otherShop.name != "Ship Inventory Content")
            {
                RemoveItem(item, this);
                item.price += 5.0f;
                AddItem(item, otherShop);
            }
            // RemoveItem(item, this);

            if (item.itemName == "Food")
            {
                userStatistics.addFood(25.0f);
            }

            RefreshDisplay();
            otherShop.RefreshDisplay();
            Debug.Log("enough gold");

            ItemSelected(item.itemName);        
        }
        Debug.Log("attempted");
    }

    void AddItem(Item itemToAdd, ShopScrollList shopList)
    {
        shopList.itemList.Add(itemToAdd);
    }

    private void RemoveItem(Item itemToRemove, ShopScrollList shopList)
    {
        for (int i = shopList.itemList.Count - 1; i >= 0; i--)
        {
            if (shopList.itemList[i] == itemToRemove)
            {
                shopList.itemList.RemoveAt(i);
            }
        }
    }

    public string ItemSelected(string itemName)
    {
        currentItemSelected = itemName;
        return currentItemSelected;
    }
}
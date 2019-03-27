using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour
{
    public Button buttonComponent;
    public TextMeshProUGUI nameLabel;
    public Image iconImage;
    public TextMeshProUGUI priceText;


    private Item item;
    private ShopScrollList scrollList;

    // Use this for initialization
    void Start()
    {
        buttonComponent.onClick.AddListener(HandleClick);
    }

    public void Setup(Item currentItem, ShopScrollList currentScrollList)
    {
        item = currentItem;
        nameLabel.text = item.itemName;
        iconImage.sprite = item.icon;
        priceText.text = item.price.ToString();
        scrollList = currentScrollList;

    }

    public void HandleClick()
    {
        scrollList.TryTransferItemToOtherShop(item);
    }
}
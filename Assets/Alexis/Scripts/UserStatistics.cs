using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserStatistics : MonoBehaviour
{
    private float maxHealth = 100.0f;
    private float maxShipHealth = 100.0f;

    [HideInInspector]
    public float health = 0.0f;
    [HideInInspector]
    public float shipHealth = 0.0f;
    public Image healthBarImage;
    public Image shipBarImage;
    [HideInInspector]
    public int gold;
    public ShopScrollList sSL;
    public TextMeshProUGUI goldText;

    private void Start()
    {
        gold = sSL.gold;
        health = maxHealth;
        shipHealth = maxHealth;
    }

    private void Update()
    {
        gold = sSL.gold;
        goldText.text = gold.ToString();
        if(health > 100.0f)
        {
            health = maxHealth;
        }
        healthBarImage.fillAmount = health / maxHealth;
        loseFoodOverTime();
        if(shipHealth > 100.0f)
        {
            shipHealth = 100.0f;
        }
        shipBarImage.fillAmount = shipHealth / maxShipHealth;
    }

    private void loseFoodOverTime()
    {
        health -= 1.0f * Time.deltaTime;
    }

    public void addFood(int foodAmount)
    { 
        health += foodAmount;
    }

    public void addGold(int gold)
    {
        sSL.gold += gold;
    }

    public void addShipHealth(int wood)
    {
        shipHealth += wood;
    }
}

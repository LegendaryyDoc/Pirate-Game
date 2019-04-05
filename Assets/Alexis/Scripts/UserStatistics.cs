using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserStatistics : MonoBehaviour
{
    private float maxHealth = 100.0f;

    [HideInInspector]
    public float gold;
    [HideInInspector]
    public float health = 0.0f;
    public Image healthBarImage;
    public ShopScrollList sSL;
    public TextMeshProUGUI goldText;

    private void Start()
    {
        gold = sSL.gold;
        health = maxHealth;
    }

    private void Update()
    {
        gold = sSL.gold;
        goldText.text = gold.ToString();
        healthBarImage.fillAmount = health / maxHealth;
        loseFoodOverTime();
    }

    private void loseFoodOverTime()
    {
        health -= 2.0f * Time.deltaTime;
    }

    public void addFood(float foodAmount)
    { 
        health += foodAmount;
    }
}

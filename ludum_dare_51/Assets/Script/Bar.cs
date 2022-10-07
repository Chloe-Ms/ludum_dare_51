using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{
    private Image barCharge;
    private Player_Weapons weapons;

    void Awake()
    {
        weapons = GameObject.Find("Player").GetComponent<Player_Weapons>();
        barCharge = gameObject.GetComponent<Image>();
    }

    void Update()
    {
        barCharge.fillAmount = weapons.CurrentCharge /3f;
    }
}

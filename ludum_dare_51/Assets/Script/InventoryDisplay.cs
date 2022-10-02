using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Image itemIcon;

    public void SetItemIcon(Sprite icon)
    {
        itemIcon.sprite = icon;
    }
}

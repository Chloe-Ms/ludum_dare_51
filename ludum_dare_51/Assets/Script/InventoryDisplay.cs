using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private Image itemIcon;

    private void Start() 
    {
        HideIcon();
    }

    public void SetItemIcon(Sprite icon)
    {
        itemIcon.color = Color.white;
        itemIcon.sprite = icon;
    }
    public void HideIcon()
    {
        itemIcon.color = new Color(1, 1, 1, 0);
    }
}

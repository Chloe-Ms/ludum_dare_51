using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject item;
    [SerializeField] private InventoryDisplay display;


    private void Start()
    {
        player = transform;
        display = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryDisplay>();
    }

    public void DropItem()
    {
        Vector2 playerPos = new Vector2(player.position.x, player.position.y + 1 );
        GameObject drop = Instantiate(item, playerPos, Quaternion.identity);
        drop.SetActive(true);
        RemoveItem();
    }

    public void RemoveItem()
    {
        display.HideIcon();
        GameObject.Destroy(item);
        item = null;
    }

    public void AddItem(GameObject item)
    {
        if(this.item != null) DropItem();
        this.item = item;
        display.SetItemIcon(item.GetComponent<SpriteRenderer>().sprite);
    }

    // public void AddWeapon(GameObject weapon)
    // {

    // }
}

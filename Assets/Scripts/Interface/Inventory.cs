using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Transform[] itemsTransform;
    public static Inventory instance;
    public InventorySpell[] items;
    public Library library;
    public Transform deck;

    private Health health;

    private void Awake()
    {
        instance = this;
    }
    public int TryToDropSpell(Transform spell, InventorySpell spellItem, int oldSocket, bool isSpell)
    {
        int min = spellItem.socket;
        for (int i = 0; i < 8; i++)
        {
            if ((itemsTransform[i].transform.position - spell.position).magnitude < (itemsTransform[min].transform.position - spell.position).magnitude && i < 2 && isSpell)
            {
                min = i;
            } 
            else if ((itemsTransform[i].transform.position - spell.position).magnitude < (itemsTransform[min].transform.position - spell.position).magnitude && i >= 2 && i < 7)
            {
                min = i;
            }
            else if((itemsTransform[i].transform.position - spell.position).magnitude < (itemsTransform[min].transform.position - spell.position).magnitude && i == 7 && spellItem.shieldValue != 0)
            {
                min = i;
            }
        }
        if((itemsTransform[min].transform.position - spell.position).magnitude < 150 * (Screen.width/1920f))
        {
            if (items[min] != null)
            {
                items[oldSocket] = items[min];
                items[oldSocket].socket = oldSocket;
                items[oldSocket].transform.position = itemsTransform[oldSocket].position;
            }
            else
            {
                items[oldSocket] = null;
            }
            items[min] = spellItem;
            spell.transform.position = itemsTransform[min].transform.position;
            if (items[7] != null)
                health.SetMaxHealth(items[7].healthValue, items[7].manaValue, items[7].shieldValue);
            if (items[7] == null)
                health.SetMaxHealth(100, 100, 100);
            return min;
        }
        else
        {
            if(min != 7)
                if (items[7] != null)
                    health.SetMaxHealth(100, 100, 100);
            else if (min == 7)
                health.SetMaxHealth(items[7].healthValue, items[7].manaValue, items[7].shieldValue);
            return -1;
        }
    }
    public bool TryToPickSpell(int id, bool isSpell, DropItems spellItem)
    {
        for (int i = 0; i < 8; i++)
        {
            if (items[i] == null && i < 2 && isSpell)
            {
                items[i] = Instantiate(library.items[id], itemsTransform[i].transform.position, Quaternion.identity, deck).GetComponent<InventorySpell>();
                items[i].socket = i;
                return true;
            }
            else if (items[i] == null && i >= 2 && i < 7)
            {
                items[i] = Instantiate(library.items[id], itemsTransform[i].transform.position, Quaternion.identity, deck).GetComponent<InventorySpell>();
                items[i].socket = i;
                return true;
            } else if (items[i] == null && i == 7 && spellItem.shieldValue != 0)
            {
                health.SetMaxHealth(spellItem.healthValue, spellItem.manaValue, spellItem.shieldValue);
                items[i] = Instantiate(library.items[id], itemsTransform[i].transform.position, Quaternion.identity, deck).GetComponent<InventorySpell>();
                items[i].socket = i;
                return true;
            }
        }
        return false;
    }
    public void OnHealthChanged(Health newHealth)
    {
        health = newHealth;
    }
}

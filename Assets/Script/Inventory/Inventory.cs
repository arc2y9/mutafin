using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Player player;
    Dictionary<string, int> items = new Dictionary<string, int>();
    public void AddItem (string key, int value = 1)
    {
        if (items.ContainsKey(key))
        {
            items[key] += value;
            Debug.Log("i01");
        }
        else
        {
            items.Add(key, value);
            Debug.Log("i02");
        }
    }
    public void RemoveItem (string key, int value = 1)
    {
        if (items.ContainsKey(key))
        {
            if (items[key] >= value)
            {
                items[key] -= value;
                Debug.Log("i03");
            }
            if (items[key] <= 0)
            {
                items.Remove(key);
                Debug.Log("i04");
            }
        }
        else
        {
            Debug.Log("i05");
        }
    }

    public void LogInventory()
    {
        if (items.Count == 0)
        {
            Debug.Log("Inventory is empty.");
            return;
        }
        foreach (var item in items)
        {
            Debug.Log("key: " + item.Key + " value:" + item.Value + "\n");
        }
    }

}

using UnityEngine;

public class Inventory
{
    private int dynamite { get; set; }

    public void AddDynamite(int amount)
    {
        dynamite += amount;
    }
}

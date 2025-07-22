using UnityEngine;

public class CollactableItem : ICollectableItem
{
    public bool hasCollected { get; set; }
    private void Start()
    {
        if (hasCollected)
        {
            Destroy(gameObject);
        }
    }
}

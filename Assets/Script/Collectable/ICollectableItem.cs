using System.Collections;
using UnityEngine;

public abstract class ICollectableItem : MonoBehaviour
{
    public float lifetime;
    public bool hasCollected { get; set; }
    public string Name;
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.GetComponent<Player>().inventory.AddItem(Name, 1);

        Destroy(this.gameObject);
    }
    */
    public virtual void Blow(Vector2 bLoc, float range)
    {
        lifetime = Random.Range(1f, 5f);

        Vector2 tLoc = GetComponent<RectTransform>().position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        
        float xAx = (tLoc.x - bLoc.x) * (Vector2.Distance(tLoc, bLoc) / range);
        float yAx = (tLoc.y - bLoc.y) * (Vector2.Distance(tLoc, bLoc) / range);

        rb.linearVelocity = new Vector2(xAx, yAx);
        StartCoroutine(SelfDestroy());
    }

    protected IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);

    }
}

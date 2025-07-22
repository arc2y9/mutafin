using System;
using System.Collections;
using UnityEngine;

public abstract class MineableObject : MonoBehaviour
{
    public static event Func<GameObject, GameObject> OnMined;
    public static event Func<GameObject, Vector2> onBlasted;
    public abstract string Name { get; set; }
    public abstract float hardness { get; }
    public abstract void Mine();


    public float lifetime;
    protected bool isHovering;
    protected bool isMouseDown;
    protected float holdTime;
    public bool blasted = false;
    protected virtual void OnMouseEnter() {isHovering = true; if (Input.GetMouseButton(0)) isMouseDown = true; }
    protected virtual void OnMouseExit() { isHovering = false; holdTime = 0f; }
    protected virtual void OnMouseDown() => isMouseDown = true;
    protected virtual void OnMouseUp() { isMouseDown = false; holdTime = 0f; }
    protected virtual void Update()
    {
        if (isHovering && isMouseDown)
        {
            holdTime += Time.deltaTime;
            if (holdTime >= hardness)
            {
                OnMined.Invoke(this.gameObject);
                Mine();
                Destroy(gameObject);
            }
        }
        if (blasted)
        {
            OnMined.Invoke(this.gameObject);
        }
    }

    public virtual void Blow(Vector2 bLoc, float range)
    {
        lifetime = UnityEngine.Random.Range(1f, 5f);

        Vector2 tLoc = GetComponent<RectTransform>().position;
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        float xAx = (tLoc.x - bLoc.x) * (Vector2.Distance(tLoc, bLoc) / range);
        float yAx = (tLoc.y - bLoc.y) * (Vector2.Distance(tLoc, bLoc) / range);

        rb.linearVelocity = new Vector2(xAx * (hardness*0.1f + 1), yAx + (hardness*0.1f +1));
        StartCoroutine(SelfDestroy());
    }

    protected IEnumerator SelfDestroy()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}

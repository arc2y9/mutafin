using System;
using UnityEngine;

public abstract class MineableObject : MonoBehaviour
{
    public static event Action<GameObject, Vector2> OnMined;
    public abstract string name { get; set; }
    public abstract float hardness { get; }
    public abstract void Mine();

    public abstract GameObject CollectableObject {  get; set; }

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
                OnMined.Invoke(CollectableObject, this.GetComponent<RectTransform>().position);
                Mine();
                Destroy(gameObject);
            }
        }
        if (blasted)
        {
            OnMined.Invoke(CollectableObject, this.GetComponent<RectTransform>().position);
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

public abstract class MineableObject : MonoBehaviour
{
    public static event Action<string> OnMined;
    public abstract string name { get; set; }
    public abstract float hardness { get; }
    public abstract void Mine();

    protected bool isHovering;
    protected bool isMouseDown;
    protected float holdTime;

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
                OnMined.Invoke(name);
                Mine();
                Destroy(gameObject);
            }
        }
    }
}

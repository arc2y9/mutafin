using System;
using System.Collections;
using UnityEngine;

public abstract class BlastableObject : MonoBehaviour
{
    public event Action<Vector3, int> OnBlast;
    public abstract float lastingTime { get; set; }
    public abstract int range { get; set; }

    private void Start()
    {
        StartCoroutine(Blast());
        Debug.Log(name + " will blast");
    }
    private IEnumerator Blast()
    {
        
        yield return new WaitForSeconds(lastingTime);

        OnBlast.Invoke(this.GetComponent<RectTransform>().position, range);
        
        Destroy(gameObject);

        Debug.Log(name + " blasted");
    }
}

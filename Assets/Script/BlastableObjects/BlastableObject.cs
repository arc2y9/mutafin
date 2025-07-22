using System;
using System.Collections;
using UnityEngine;

public abstract class BlastableObject : MonoBehaviour
{
    public event Action<GameObject> OnBlast;
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

        OnBlast.Invoke(this.gameObject);
        
        Destroy(gameObject);

        Debug.Log(name + " blasted");
    }
}

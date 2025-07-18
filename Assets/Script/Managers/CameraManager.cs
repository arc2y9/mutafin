using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public string targetTag = "Player";
    public Vector3 offset = new Vector3(0, -400, -10);
    public float smoothSpeed = 0.125f;

    private Transform target;

    void Update()
    {
        // Oyuncuyu sahnede bulmak (Instantiate sonrasý)
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag(targetTag);
            if (playerObj != null)
                target = playerObj.transform;
            else
                return;
        }

        // Takip iþlemi
        Vector3 desiredPosition = target.position + offset;
        desiredPosition.y -= 4f;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
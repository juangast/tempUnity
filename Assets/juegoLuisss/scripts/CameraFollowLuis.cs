using UnityEngine;

public class CameraFollowLuis : MonoBehaviour
{
    public Transform target;
    public float offsetX = 0f;
    public float smoothSpeed = 0.125f;

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, target.position.x + offsetX, smoothSpeed);
        transform.position = pos;
    }
}
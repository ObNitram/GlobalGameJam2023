using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float _smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10));
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, _smoothTime);
        //transform.position = Vector3.Lerp(transform.position, targetPosition, _smoothTime);
    }
}

using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;
    public float SmoothTime = 1f;
    public Vector3 Offset;
    private Vector3 _velocity = Vector3.zero;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Target != null)
        {
            Vector3 targetPosition = Target.position + Offset;

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, SmoothTime);
        }
    }
}

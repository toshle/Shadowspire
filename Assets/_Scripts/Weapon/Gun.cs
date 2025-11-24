using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public Transform BulletPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(BulletPrefab, BulletPoint.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        }
    }
}

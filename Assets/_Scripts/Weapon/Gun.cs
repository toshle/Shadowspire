using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    [SerializeField] public GameObject BulletPrefab;
    [SerializeField] public Transform BulletPoint;
    public float AttackSpeed = 1f;
    private float _lastAttackTime = 0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.IsPaused)
            return;
        if (Mouse.current.leftButton.isPressed)
        {
            Debug.Log("Shoot at " + (Time.time - _lastAttackTime) + " Atk Speed: " + AttackSpeed);
            if (Time.time - _lastAttackTime >= AttackSpeed )
            {
                Instantiate(BulletPrefab, BulletPoint.position, Quaternion.LookRotation(transform.forward, Vector3.up));
                _lastAttackTime = Time.time;
            }
        }
    }
}

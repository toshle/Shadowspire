using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 100f;
    public SphereCollider sphereCollider;
    public Rigidbody rb;
    public int damege = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * Speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
        if (collision.collider.CompareTag("Enemy"))
        {
            Health p = collision.collider.GetComponent<Health>();
            if (p != null) p.TakeDamage(damege);
        }
        
    }
   
}

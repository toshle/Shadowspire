using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed = 100f;
    public SphereCollider sphereCollider;
    public Rigidbody rb;
    public float Damage = 50;
    public int PassThrough = 0;
    private int _targetsHit = 0;
    public GameObject HitPrefab;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = transform.forward * Speed * Time.deltaTime;
    }


    // ??????????? ???? ???????? ? ????
    // ??????? Health ?????????? 
    // ?????? ??????
    private void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("Hit " + collision.gameObject.name);
        if (collision.CompareTag("Player") || collision.CompareTag("Projectile"))
        {
            return;
        }
        if (collision.CompareTag("Enemy") || collision.CompareTag("Boss"))
        {
            Health p = collision.GetComponent<Health>();
            if (p.currentHealth <= 0)
            {
                return;
            }
            _targetsHit++;
            //Debug.Log("Enemy HP: " + p.currentHealth + "/" + p.maxHealth);

            if (p != null) p.TakeDamage(Damage);
            var hit = Instantiate(HitPrefab, collision.transform);
            Destroy(hit, 5f);
            if (_targetsHit > PassThrough)
            {
                Destroy(gameObject);
            }
        } else
        {
            Destroy(gameObject);
        }
    }
   
}

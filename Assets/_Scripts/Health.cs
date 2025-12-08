using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    [SerializeField] HealthBar _healthBar;
    [SerializeField] HUD _hud;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amt)
    {
        currentHealth -= amt;
        if(_healthBar != null)
        {
            _healthBar.SetHealth(currentHealth, maxHealth);
        } else if(_hud != null)
        {
            _hud.SetHealth(currentHealth, maxHealth);
        }
        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        if (CompareTag("Enemy"))
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            GameObject s = GameObject.FindGameObjectWithTag("Spawner");
            var level = p.GetComponent<Level>();
            var enemy = GetComponent<EnemyAI>();
            level.GiveExp(enemy.exp);
            var spawner = s.GetComponent<Spawner>();
            spawner.KilledEnemy();
        }
        Destroy(gameObject);
    }
}
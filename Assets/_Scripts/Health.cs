using System;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    [SerializeField] HealthBar _healthBar;
    [SerializeField] HUD _hud;

    private void Awake()
    {
        currentHealth = maxHealth;
        if (_healthBar != null)
        {
            _healthBar.SetHealth(currentHealth, maxHealth);
        }
    }

    public void Heal(float amt)
    {
        if(currentHealth + amt > maxHealth)
        {
            currentHealth = maxHealth; 
        } else
        {
            currentHealth += amt;
        }
        if (_healthBar != null)
        {
            _healthBar.SetHealth(currentHealth, maxHealth);
        }
        else if (_hud != null)
        {
            _hud.SetHealth(currentHealth, maxHealth);
        }
    }

    public void TakeDamage(float amount)
    {
        if (CompareTag("Enemy"))
        {
            GameManager.Player.Health.Heal(GameManager.Player.Stats.LifeSteal);
        }
        if(CompareTag("Player"))
        {
            amount -= GameManager.Player.Stats.Armor;
        }
        currentHealth -= amount;
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
        if (CompareTag("Enemy") || CompareTag("Boss"))
        {
            GameObject s = GameObject.FindGameObjectWithTag("Spawner");
            var enemy = GetComponent<EnemyAI>();
            GameManager.Player.Level.GiveExp(enemy.exp);
            var spawner = s.GetComponent<Spawner>();
            spawner.KilledEnemy();
            var navAgent = GetComponent<NavMeshAgent>();
            navAgent.isStopped = true;
            enemy.IsDead = true;
        }

        gameObject.transform.localScale = new Vector3(1f, 0.01f, 1f);

        Destroy(gameObject, 2f);

        if (CompareTag("Player"))
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }
}
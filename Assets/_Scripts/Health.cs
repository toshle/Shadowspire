using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;

public class Health : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public bool isPoisoned = false;
    private float _poisonStartTime = 0f;
    private float _lastPoisonTick = 0f;

    [SerializeField] HealthBar _healthBar;
    [SerializeField] HUD _hud;
    [SerializeField] SkinnedMeshRenderer _model;
    [SerializeField] Material _poisonMaterial;


    private void Awake()
    {
        currentHealth = maxHealth;
        if (_healthBar != null)
        {
            _healthBar.SetHealth(currentHealth, maxHealth);
        }
    }
    private void Update()
    {
        if (currentHealth > 0 && isPoisoned && (Time.time - _lastPoisonTick) > 1f)
        {
            _lastPoisonTick = Time.time;
            TakeDamage(1f);
            //_model.materials[1] = _poisonMaterial;
        }

        if (isPoisoned && (Time.time - _poisonStartTime) > 10f)
        {
            isPoisoned = false;
            _healthBar.SetPoison(false);
            //_model.materials[1] = _model.materials[0];
        }
    }

    public void Poison()
    {
        isPoisoned = true;
        _poisonStartTime = Time.time;
        _healthBar.SetPoison(true);
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
        if (CompareTag("Enemy") || CompareTag("Boss"))
        {
            GameManager.Player.Health.Heal(GameManager.Player.Stats.LifeSteal);
        }
        if(CompareTag("Player"))
        {
            amount -= GameManager.Player.Stats.Armor;
        }
        currentHealth -= amount;
        if (currentHealth < 0)
            currentHealth = 0;

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
            enemy.Die();
            var collider = GetComponent<BoxCollider>();
            collider.size = new Vector3(0.1f, 0.1f, 0.1f);
            collider.excludeLayers = LayerMask.GetMask(new string[] { "Player", "Enemies" });
            _healthBar.gameObject.SetActive(false);
        }
        //gameObject.transform.localScale = new Vector3(1f, 0.01f, 1f);

        Destroy(gameObject, 5f);

        if(CompareTag("Boss"))
            GameManager.Instance.UpdateGameState(GameState.Win);

        if (CompareTag("Player"))
        {
            GameManager.Instance.UpdateGameState(GameState.Lose);
        }
    }
}

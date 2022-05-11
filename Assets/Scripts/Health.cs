using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int health;
    Effects effects;
    // [SerializeField] ParticleSystem explosion;
    void Awake() {
        effects = GetComponent<Effects>();
    }

    void Start () {
        health = maxHealth;
    }

    public int GetHealth() {
        return health;
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null) {
            DecreaseHealth(damageDealer.GetDamage());
            PlayHitEffect();
            damageDealer.Hit();
        }
        
    }

    void IncreaseHealth(int healthAmmount) {
        if ((health + healthAmmount) > maxHealth) {
            health = maxHealth;
        } else {
            health += healthAmmount;
        }
    }

    void DecreaseHealth(int damage) {
        health -= damage;
        if (health < 0) {
            Destroy(gameObject);
        }
    }

    void PlayHitEffect() {
        ParticleSystem explosion = effects.GetExplosion();
        if (explosion != null) {
            ParticleSystem instance = Instantiate(explosion, transform.position, Quaternion.identity);
            // Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }
}

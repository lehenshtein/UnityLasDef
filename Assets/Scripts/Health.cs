using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] int health;
    [SerializeField] bool applyCameraShake = false;
    Effects effects;
    CameraShake cameraShake;
    AudioPlayer audioPlayer;
    ScoreKeeper scoreKeeper;
    LevelManager levelManager;

    void Awake() {
        effects = GetComponent<Effects>();
        cameraShake = Camera.main.GetComponent<CameraShake>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        levelManager = FindObjectOfType<LevelManager>();
    }

    void Start () {
        health = maxHealth;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public int GetHealth() {
        return health;
    }

    void OnTriggerEnter2D(Collider2D other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();

        if (damageDealer != null) {
            DecreaseHealth(damageDealer.GetDamage());
            ShakeCamera();
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
            audioPlayer.PlayExplosionClip();
            AddScore();
            GoToEndGameMenu();
            Destroy(gameObject);
        }
    }

    void AddScore() {
        if (!applyCameraShake && scoreKeeper != null) { //check that it isn't a player using camerashake var
            scoreKeeper.IncreaseScore(maxHealth);
        }
    }

    void GoToEndGameMenu() {
        if (applyCameraShake && levelManager != null) { //check that it IS a player using camerashake var
            levelManager.LoadEndMenu();
        }
    }

    void PlayHitEffect() {
        ParticleSystem explosion = effects.GetExplosion();
        if (explosion != null) {
            ParticleSystem instance = Instantiate(explosion, transform.position, Quaternion.identity);
            // Destroy(instance.gameObject, instance.main.duration + instance.main.startLifetime.constantMax);
        }
    }

    void ShakeCamera() {
        if (cameraShake != null && applyCameraShake){
            cameraShake.Play();
        }
    }
}

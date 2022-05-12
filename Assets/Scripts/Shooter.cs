using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float baseFireRate = .2f;
    
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float fireRateVariance = 0f;
    [SerializeField] float minFireRate = .1f;
    Coroutine firingCoroutine;
    [HideInInspector] public bool isFiring = false;

    AudioPlayer audioPlayer;

    void Awake() {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start() {
        if (useAI) {
            isFiring = true;
        }
    }

    void Update() {
        Fire();
    }

    void Fire() {
        if (isFiring && firingCoroutine == null) {
            firingCoroutine = StartCoroutine(FireContinuosly());
        } else if (!isFiring && firingCoroutine!= null) {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuosly()
    {
        while (true) {
            GameObject missle = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = missle.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.velocity = new Vector2(0, useAI ? -projectileSpeed : projectileSpeed);
            }
            if (audioPlayer != null) {
                audioPlayer.PlayShootingClip();
            }
            
            Destroy(missle, projectileLifetime);

            if (useAI) {
                float timeToNextMissle = UnityEngine.Random.Range(baseFireRate - fireRateVariance, baseFireRate + fireRateVariance);
                timeToNextMissle = Mathf.Clamp(timeToNextMissle, minFireRate, timeToNextMissle + fireRateVariance);

                yield return new WaitForSecondsRealtime(timeToNextMissle);
            }

            yield return new WaitForSecondsRealtime(baseFireRate);
        }
    }
}

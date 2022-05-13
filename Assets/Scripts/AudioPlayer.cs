using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField][Range(0f, 1f)] float shootingVolume = .4f;

    [Header("Explosion")]
    [SerializeField] AudioClip explosionClip;
    [SerializeField][Range(0f, 1f)] float explosionVolume = .5f;
    static AudioPlayer instance;

    void Awake() {
        ManageSingleton();
    }

    void ManageSingleton() {
        // int instanceCount = FindObjectsOfType(GetType()).Length;
        // if (instanceCount > 1) {
        if (instance != null) { //if we r starting then instance null and we set it as this.
                                //else instance is not null and we'll destoy else instance
            gameObject.SetActive(false);
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
//GLOBAL we need this if we want to have access to singleton without FindElement check the Shooter.cs FireContinuosly
    public AudioPlayer GetInstance() {
        return instance;
    }

    public void PlayShootingClip() {
        PlayClip(shootingClip, shootingVolume);
    }

    
    public void PlayExplosionClip() {
        PlayClip(explosionClip, explosionVolume);
    }

    void PlayClip(AudioClip clip, float clipVolume) {
        if (clip != null) {
            Vector3 cameraPosition = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPosition, clipVolume);
        }
    }
}

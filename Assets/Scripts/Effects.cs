using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField] ParticleSystem explosion;
    public ParticleSystem GetExplosion() {
        return explosion;
    }
}

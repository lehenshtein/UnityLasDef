using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] Vector2 moveSpeed;
    Vector2 offset = new Vector2(0, 0);
    Material material;
    
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
    }

    void LateUpdate()
    {
        ScrollBackground();
    }

    void ScrollBackground() {
        offset = moveSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}

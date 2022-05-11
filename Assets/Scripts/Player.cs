using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    [SerializeField] float movementModifier = 8f;
    [SerializeField] float paddingLeft = .5f;
    [SerializeField] float paddingRigt = .5f;
    [SerializeField] float paddingTop = 6f;
    [SerializeField] float paddingBottom = 2f;

    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;

    Shooter shooter;

    void Awake() {
        shooter = GetComponent<Shooter>();
    }

    void Start() {
        InitBounds();
    }

    void Update()
    {
        Move();

    }

    void InitBounds() {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector2 delta = rawInput * movementModifier * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRigt);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        transform.position = newPos;
    }

    void OnMove(InputValue value) {
            rawInput = value.Get<Vector2>();
    }

    void OnFire(InputValue value) {
        if (shooter == null) {
            return;
        }

        shooter.isFiring = value.isPressed;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIhud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Health health;
    [SerializeField] Slider slider;
    ScoreKeeper scoreKeeper;
    int maxHealth;

    void Awake()
    {
        // scoreText = GetComponent<TextMeshProUGUI>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    void Start() {
        SetMaxHealth();
        scoreText.text = "Score:\n0";
        slider.value = 1f;
    }

    void Update()
    {
        ChangeScore();
        ChangeHealth();
    }

    void ChangeScore() {
        if(scoreKeeper != null) {
            int totalScore = scoreKeeper.GetScore();
            scoreText.text = "Score:\n" + totalScore.ToString("0000000");
        }
    }

    void SetMaxHealth() {
        if(health != null) {
            maxHealth = health.GetMaxHealth();
        }
    }

    void ChangeHealth() {
        if(health != null) {
            int totalHealth = health.GetHealth();
            float healthForSlider = Mathf.InverseLerp(0f, maxHealth, totalHealth);
            if(healthForSlider <= Mathf.Epsilon) {
                slider.value = 0;
                return;
            }
            
            slider.value = healthForSlider;
        }
    }
}

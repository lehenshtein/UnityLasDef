using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    ScoreKeeper scoreKeeper;
    void Awake() {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    // void ManageSingleton() {

    // }

    public void LoadMainMenu() {
        SceneManager.LoadScene(0);
    }
    public void LoadGame() {
        if (scoreKeeper != null) {
            scoreKeeper.ResetScore();
        }
        SceneManager.LoadScene("Game");
    }
    public void LoadEndMenu() {
        StartCoroutine(loadingEndMenu());
    }

    IEnumerator loadingEndMenu() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }
    public void QuitGame() {
        Debug.Log("Quiting game...");
        Application.Quit();
    }
}

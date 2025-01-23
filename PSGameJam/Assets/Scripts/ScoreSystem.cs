using UnityEngine;
using UnityEngineUI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ScoreSystem : MonoBehaviour
{
    public static ScoreSystem instance;

    public Text scoreText;
    public Text highscoreText;

    int score = 0;
    int highscore = 0;

    private void Awake() {
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = score.ToString() + " POINTS";
        highscoreText.text = "HIGHSCORE: " + highscore;

    }

    // Update is called once per frame
    public void AddPoint() {
        score += 1;
        scoreText.text = score + " POINTS";
    }
}

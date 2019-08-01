using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndGame : MonoBehaviour
{
    [SerializeField]
    GameObject scoreText;

    [SerializeField]
    GameObject hitsText;

    [SerializeField]
    GameObject higherText;

    int score;
    int hits;

    // Start is called before the first frame update
    void Start()
    {
        // pause the game when added to the scene
        Time.timeScale = 0;
    }

    public void HandleQuitButtonOnClickEvent()
    {
        // unpause game, destroy menu, and go to main menu
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
    }

    public void setEndingValues(int score, int hits)
    {
        int currentHighScore = 0;
        this.score = score;
        this.hits = hits;

        scoreText.GetComponent<TextMeshProUGUI>().SetText(this.score.ToString());
        hitsText.GetComponent<TextMeshProUGUI>().SetText(this.score.ToString());

        if (PlayerPrefs.HasKey("highestScore"))
        {
            currentHighScore = PlayerPrefs.GetInt("highestScore");
        }

        if (score > currentHighScore)
        {
            currentHighScore = score;
            PlayerPrefs.SetInt("highestScore", currentHighScore);
        }
        higherText.GetComponent<TextMeshProUGUI>().SetText(currentHighScore.ToString());
    }
}

using UnityEngine;
using TMPro;

public class PlayerDeath : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;
    public bool gameEnded = false;

    public void Die()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "GAME OVER";
        }

        Time.timeScale = 0f;
    }
    public void Win()
    {
        if (gameEnded) return;

        gameEnded = true;

        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
            gameOverText.text = "You Escaped!";
        }

        Time.timeScale = 0f;
    }
}
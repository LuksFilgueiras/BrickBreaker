using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private float startTimer = 3f;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image screenBackground;
    [SerializeField] private Color32 screenColor;
    private float timer = 0f;
    [SerializeField] private bool isPlayerDead;
    [SerializeField] private bool playerWon;

    public GameObject startScreen;

    void Awake(){
        message.text = "STARTS IN";
        startScreen.SetActive(true);
    }

    void Update(){
        if(!isPlayerDead && !playerWon){
            StartScreen();
        }else{
            Time.timeScale = 0;

            if(Input.GetKeyDown(KeyCode.Space)){
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void StartScreen(){
        if(startScreen.activeSelf){
            Time.timeScale = 0;

            timer += Time.unscaledDeltaTime;

            float timeToText = startTimer - timer;

            timerText.text = Mathf.CeilToInt(timeToText).ToString();

            if(timer >= startTimer){
                Time.timeScale = 1f;
                timer = 0f;

                startScreen.SetActive(false);
            }
        }
    }

    public void DeathScreen(bool isPlayerDead){
        this.isPlayerDead = isPlayerDead;
        message.text = "GAME OVER";
        timerText.text = "press 'SPACE' to restart the game";
        timerText.fontSize = 24;
        screenBackground.color = screenColor;
        startScreen.SetActive(true);
    }

    public void WinGame(){
        playerWon = true;
        message.text = "WINNER!";
        timerText.text = "press 'SPACE' to restart the game";
        timerText.fontSize = 24;
        screenBackground.color = screenColor;
        startScreen.SetActive(true);
    }
}

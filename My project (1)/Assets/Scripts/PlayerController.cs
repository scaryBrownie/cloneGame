using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravityScale = 2.5f;

    private Rigidbody2D rb;
    private bool isGameStarted = false;

    public int currentScore, highScore;
    public TextMeshProUGUI scoreText;

    public bool isPlayerDead;
    public GameObject deathPanel;
    public TextMeshProUGUI deathScore, deathHighScore;

    private PlayerColorChanger playerColorChanger;

    private AudioSource jumpSound;

    public int starPuanMiktari = 5;

    public float fallThreshold = -10f;
    public float fallDuration = 3f;
    private float fallTimer = 0f;
    
    public GameObject rewardIco;


    private void Awake()
    {
        playerColorChanger = GetComponent<PlayerColorChanger>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;

        jumpSound = GetComponent<AudioSource>();

        scoreText = GameObject.Find("Canvas/Score").GetComponent<TextMeshProUGUI>();

        highScore = PlayerPrefs.GetInt("highScore", 0);

        float waitTime = 0.1f;
        StartCoroutine(WaitAndReload(waitTime));

        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isGameStarted)
            {
                StartGame();
            }

            Jump();
        }

        fallCheck();

        scoreText.text = currentScore.ToString();
    }



    private void Jump()
    {
        if (isGameStarted && !isPlayerDead)
        {
            if (!jumpSound.isPlaying)
            {
                jumpSound.Play();
            }

            rb.velocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }


    private void StartGame()
    {
        rb.gravityScale = gravityScale;
        isGameStarted = true;
    }

    public void DeathPlayer()
    {
        isPlayerDead = true;
        if (currentScore > highScore)
        {
            PlayerPrefs.SetInt("highScore", currentScore);
            highScore = currentScore;
        }
        deathPanel.SetActive(true);
        deathScore.text = "Score: " + currentScore;
        deathHighScore.text = "High Score: " + highScore;

        rb.constraints = RigidbodyConstraints2D.FreezePositionX;

        Time.timeScale = 0;
    }

    public void RewardedAdControl()
    {
        isPlayerDead = false;
        deathPanel.SetActive(false);
        rb.gravityScale = 0;
        isGameStarted = false;

        rewardIco.SetActive(false);

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

       Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Time.timeScale = 0f;

        SceneManager.LoadSceneAsync(currentSceneIndex).completed += (operation) =>
        {
            Time.timeScale = 1f;
        };
    }

    private IEnumerator WaitAndReload(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        playerColorChanger.UpdateColor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("star"))
        {
            currentScore += starPuanMiktari;
            Destroy(collision.gameObject);
        }
    }

    private void fallCheck()
    {
        if (!isPlayerDead && transform.position.y < fallThreshold)
        {
            fallTimer += Time.deltaTime;

            if (fallTimer >= fallDuration)
            {
                DeathPlayer();
                rewardIco.SetActive(false);
            }
        }
        else
        {
            fallTimer = 0f;
        }
    }
}
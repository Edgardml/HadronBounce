using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public Text scoreText;
    public Text timeText;

    public float score;
    public float time;
    public float scoreRate;

    public GameObject pauseMenu;
    public bool isPaused = false;

    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScore;

    Camera mainCamera;
    Vector3 cameraInitialPosition;
    float shakeMagnetude = 0.05f, shakeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();

        score = 0f;
        scoreRate = 1f;
        time = 0f;
        pauseMenu.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + (int)score;
        score += scoreRate * Time.deltaTime;

        timeText.text = "Time: " + (int)time;
        time += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) ResumeGame();
            else PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Shake()
    {
        cameraInitialPosition = mainCamera.transform.position;
        InvokeRepeating("StartCameraShaking", 0f, 0.005f);
        Invoke("StopCameraShaking", shakeTime);
    }

    void StartCameraShaking()
    {
        float offsetX = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        float offsetY = Random.value * shakeMagnetude * 2 - shakeMagnetude;
        Vector3 intermediatePosition = mainCamera.transform.position;
        intermediatePosition.x += offsetX;
        intermediatePosition.y += offsetY;
        mainCamera.transform.position = intermediatePosition;
    }

    void EndCameraShaking()
    {
        CancelInvoke("StartCameraShaking");
        mainCamera.transform.position = cameraInitialPosition;
    }

    IEnumerator GameOverRoutine()
    {
        yield return new WaitForSeconds(1f);
        gameOverPanel.SetActive(true);
        finalScore.text = "Final Score: " + ((int)score).ToString();
        Time.timeScale = 0f;
        yield return new WaitForSeconds(2);
    }
}

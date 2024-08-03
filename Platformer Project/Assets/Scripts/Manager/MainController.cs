using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public static MainController instance;

    public GameObject GameOverPanel;

    [Header("Score")]
    public TextMeshProUGUI CurrentScore;
    public TextMeshProUGUI BestScore;
    private float score;

    [Header("Level")]
    public TextMeshProUGUI Level;

    public bool isGameStart = false; // 게임이 시작했는지 안했는지 [3..2..1..]
    [Header("Start UI")]
    public TextMeshProUGUI StartCountText; // 3..2..1..
    public GameObject StartUIPanel;        // 시작 전 관련 오브젝트의 부모

    private void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        StartCountText.text = "3";
        yield return new WaitForSeconds(1f);
        StartCountText.text = "2";
        yield return new WaitForSeconds(1f);
        StartCountText.text = "1";
        yield return new WaitForSeconds(1f);
        StartCountText.text = "START";
        isGameStart = true;
        yield return new WaitForSeconds(0.3f);
        StartUIPanel.SetActive(false);
    }

    private void Update()
    {
        score = GameManager.instance.score;
        UpdateGUIText();
    }

    private void UpdateGUIText()
    {
        CurrentScore.text = $"현재점수 : {GameManager.instance.score}";
        Level.text = $"현재레벨 : {GameManager.instance.ReturnCurrentDifficulty()}";
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void GameOver()
    {
        GameOverPanel.SetActive(true);   // 게임 오버시 게임 종료 UI 활성화
        // 현재 점수가 최고 점수보다 높을 때 갱신
        if (score > PlayerPrefs.GetFloat(GameData.BestScore))
        {
           PlayerPrefs.SetFloat(GameData.BestScore, score);
           BestScore.text = $"최고점수 : {GameManager.instance.score}";
        }
        else // 현재 점수가 최고 점수보다 낮을 때, 기존의 최고 점수를 불러온다.
        {
            BestScore.text = $"최고점수 : {PlayerPrefs.GetFloat(GameData.BestScore)}"; 
        }

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#endif
    }

}

using UnityEngine;

public class GameManager : MonoBehaviour
{
    // 싱글톤 패턴
    public static GameManager instance;  // GameManager.instance

    public int difficulty; // 0 : 이지, 1 : 노말, 2 : 하드             

    public float score;

    private void Update()
    {
        score += Time.deltaTime * (difficulty+1);

        if (Input.GetKeyDown(KeyCode.S)) // 키보드의 S눌렀을 때 True // GameOver.. 플레이어가 죽었을 때
        {
            if(score > PlayerPrefs.GetFloat(GameData.BestScore)) // 현재 점수. BestScore 점수보다 클때만 저장
            PlayerPrefs.SetFloat(GameData.BestScore, score);
        }
    }


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (PlayerPrefs.HasKey(GameData.GameDifficulty)) // HasKey "키값" 없으면 false, true
        {
            difficulty = PlayerPrefs.GetInt(GameData.GameDifficulty);
        }
    }

    public string ReturnCurrentDifficulty()
    {
        string name = null;

        switch (difficulty)
        {
            case 0:
                name = "이지";
                break;
            case 1:
                name = "노말";
                break;
            case 2:
                name = "하드";
                break;
            default:
                name = $"키 : {difficulty} 존재하지 않는 키 값입니다.";
                break;
        }

        return name;
    }

    public void SaveGameDifficulty()
    {
        PlayerPrefs.SetInt(GameData.GameDifficulty, difficulty); // GameDifficulty 이름으로. difficulty변수(정수) 저장.
    }
}
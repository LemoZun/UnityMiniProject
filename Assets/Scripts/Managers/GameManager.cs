using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private PlayerController player;
    [SerializeField] private GolemController golem;
    [SerializeField] private GameObject gameoverUI;
    [SerializeField] private GameObject gameclearUI;
    private bool isGameOver;
    private bool isGameClear;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {

    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += InitializeField;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= InitializeField;
        if(player != null && golem != null)
        {
            player.OnPlayerDied -= GameOver;
            golem.golemModel.OnGolemDied -= GameClear;
        }
        else
        {
            Debug.Log("오브젝트가 이미 파괴된 상황?");
        }
    }

    private void Update()
    {
        if(isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if(isGameClear && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
      
    }

    private void InitializeField(Scene scene, LoadSceneMode mode)
    {
        player = FindObjectOfType<PlayerController>();
        golem = FindObjectOfType<GolemController>();
        gameoverUI = GameObject.FindWithTag("OverUI");
        gameclearUI = GameObject.FindWithTag("ClearUI");

        isGameOver = false;
        isGameClear = false;

        gameoverUI.SetActive(false);
        gameclearUI.SetActive(false);

        player.OnPlayerDied += GameOver;
        golem.golemModel.OnGolemDied += GameClear;

    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        gameoverUI.SetActive(true);
        isGameOver = true;
    }

    private void GameClear()
    {
        Debug.Log("Game Clear");
        gameclearUI.SetActive(true);
        isGameClear = true;
        
    }

    public void Restart()
    {
        Debug.Log("게임 재시작");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

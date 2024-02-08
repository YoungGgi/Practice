using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("# Game Object")]
    [SerializeField]
    private Player player;

    [SerializeField]
    private PoolManager pool;

    [SerializeField]
    private LevelUp uiLevelUp;

    [SerializeField]
    private Result uiResult;
    [SerializeField]
    private Transform uiJoy;
    [SerializeField]
    private GameObject enemyCleaner;

    public Player GetPlayer
    { get{ return player;} }

    public PoolManager GetPool
    {  get{ return pool; } }

    public LevelUp GetUiLevelUp
    {
        get {return uiLevelUp;}
    }

    [Header("# Game Control")]
    [SerializeField]
    private float gameTime;
    [SerializeField]
    private float maxGameTime = 2 * 10f;
    [SerializeField]
    private bool isLive;

    public float GameTime
    { get {return gameTime;} }

    public float MaxGameTime
    { get {return maxGameTime;}}

    public bool GetIsLive
    { get {return isLive;}}
    

    [Header("# Player Control")]
    [SerializeField]
    private int playerID;
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private int level;
    [SerializeField]
    private int kill;
    [SerializeField]
    private int exp;
    [SerializeField]
    private int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450, 600};

    public int GetPlayerID
    { get {return playerID;}}

    public float GetHealth
    {
        get {return health;}
        set {health = value;}
    }

    public float GetMaxHealth
    {get {return maxHealth;} }

    public int GetLevel
    { get {return level;}}
    
    public int Get_Exp
    { get {return exp;} }

    public int GetKill
    {   get {return kill;}
        set {kill = value;}}

    public int[] GetNextExp
    { get {return nextExp;}}




    private void Awake() 
    {
        instance = this;
        Application.targetFrameRate = 60;  // 게임 프레임 설정
    }

    public void GameStart(int id) 
    {
        playerID = id;
        health = maxHealth;
        
        player.gameObject.SetActive(true);
        uiLevelUp.Select(playerID % 2);    // 무기 장착
        Resume();

        AudioManager.instance.PlayBgm(true);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void GameOver()
    {
        StartCoroutine(GameOverRoutine());
    }

    IEnumerator GameOverRoutine()
    {
        isLive = false;
        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Lose();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Lose);
    }

    public void GameWin()
    {
        StartCoroutine(GameWinRoutine());
    }

    IEnumerator GameWinRoutine()
    {
        isLive = false;
        enemyCleaner.SetActive(true);

        yield return new WaitForSeconds(0.5f);

        uiResult.gameObject.SetActive(true);
        uiResult.Win();
        Stop();

        AudioManager.instance.PlayBgm(false);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Win);
    }

    public void GameRetry()
    {
        SceneManager.LoadScene(0);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    private void Update() 
    {
        if(!isLive)
           return;
        
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
            GameWin();
        }
    }

    public void GetExp()
    {
        if(!isLive)
            return;
        
        exp++;

        // Mathf.Min(level, nextExp.Length - 1) => 최대 레벨이 됐다면 최대 레벨만 출력
        if(exp == nextExp[Mathf.Min(level, nextExp.Length - 1)])
        {
            level++;
            exp = 0;
            uiLevelUp.Show();
        }
    }

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
        uiJoy.localScale = Vector3.zero;
    }

    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
        uiJoy.localScale = Vector3.one;
    }

}

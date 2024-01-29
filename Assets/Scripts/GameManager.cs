using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [Header("# Game Object")]
    [SerializeField]
    private Player player;

    public Player GetPlayer
    {
        get{ return player;}
    }

    [SerializeField]
    private PoolManager pool;

    public PoolManager GetPool
    {
        get{ return pool; } 
    }

    [Header("# Game Control")]
    [SerializeField]
    private float gameTime;
    [SerializeField]
    private float maxGameTime = 2 * 10f;

    public float GameTime
    {
        get {return gameTime;}
    }

    public float MaxGameTime
    {
        get {return maxGameTime;}
    }
    

    [Header("# Player Control")]
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private int level;
    [SerializeField]
    private int kill;
    [SerializeField]
    private int exp;
    [SerializeField]
    private int[] nextExp = {3, 5, 10, 100, 150, 210, 280, 360, 450, 600};


    public int GetHealth
    {
        get {return health;}
    }

    public int GetMaxHealth
    {
        get {return maxHealth;}
    }

    public int GetLevel
    {
        get {return level;}
    }
    
    public int Get_Exp
    {
        get {return exp;}
    }

    public int GetKill
    {
        get {return kill;}
        set {kill = value;}
    }

    public int[] GetNextExp
    {
        get {return nextExp;}
    }

    private void Awake() 
    {
        instance = this;
    }

    private void Start() 
    {
        health = maxHealth;
    }

    private void Update() 
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }

    public void GetExp()
    {
        exp++;

        if(exp == nextExp[level])
        {
            level++;
            exp = 0;

        }
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField]
    private Player player;

    public Player GetPlayer
    {
        get{ return player;}
        //set{ player = value;}
    }

    [SerializeField]
    private PoolManager pool;

    public PoolManager GetPool
    {
        get{ return pool; } 
        //set{ pool = value;}
    }

    [SerializeField]
    private float gameTime;
    [SerializeField]
    private float maxGameTime = 2 * 10f;

    public float GameTime
    {
        get {return gameTime;}
    }
    

    private void Awake() 
    {
        instance = this;
    }

    private void Update() 
    {
        gameTime += Time.deltaTime;

        if(gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }
    }


}

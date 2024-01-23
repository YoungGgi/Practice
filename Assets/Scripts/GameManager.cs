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
    

    private void Awake() 
    {
        instance = this;
    }


}

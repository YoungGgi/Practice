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
        set{ player = value;}
    }
    

    private void Awake() 
    {
        instance = this;
    }


}

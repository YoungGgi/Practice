using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private void Update() 
    {
        if(Input.GetButtonDown("Jump"))
        {
            GameManager.instance.GetPool.Get(1);
        }
    }
}

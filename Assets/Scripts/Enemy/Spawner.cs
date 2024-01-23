using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField]
    private Transform[] spawnPoint;

    float timer;


    // 자식 오브젝트들 초기화
    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    
    private void Update() 
    {
        timer += Time.deltaTime;

        if(timer > 0.2f)
        {
            Spawn();
            timer = 0;
        }
        
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.GetPool.Get(Random.Range(0, 2));
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
    }
}

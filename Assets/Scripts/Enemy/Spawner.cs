using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    [SerializeField]
    private Transform[] spawnPoint;

    [SerializeField]
    private SpawnData[] spawnData;

    int level;
    float timer;


    // 자식 오브젝트들 초기화
    private void Awake() {
        spawnPoint = GetComponentsInChildren<Transform>();
    }
    
    // FloorToInt = 소수점 아래는 버리고 int형으로 변환
    // CeilToInt = 소수점 아래를 올리고 int형으로 변환
    private void Update() 
    {
        timer += Time.deltaTime;
        level = Mathf.Min(Mathf.FloorToInt(GameManager.instance.GameTime / 10f), spawnData.Length - 1);

        if(timer > spawnData[level].SpawnTime)
        {
            Spawn();
            timer = 0;
        }
        
    }

    private void Spawn()
    {
        GameObject enemy = GameManager.instance.GetPool.Get(0);
        enemy.transform.position = spawnPoint[Random.Range(1, spawnPoint.Length)].position;
        enemy.GetComponent<Enemy>().Init(spawnData[level]);
    }
}

// 몬스터 스폰 속성 : 스프라이트 타입, 소환시간, 체력, 속도
[System.Serializable]
public class SpawnData
{
    [SerializeField]
    private int spriteType;
    [SerializeField]
    private float spawnTime;
    [SerializeField]
    private int health;
    [SerializeField]
    private float speed;

    public int SpriteType { get {return spriteType;} }
    
    public float SpawnTime { get {return spawnTime;} }

    public int Health { get {return health;} }

    public float Speed { get {return speed;} }

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    private int id;
    [SerializeField]
    private int prefabID;
    [SerializeField]
    private float damage;
    [SerializeField]
    private int count;
    [SerializeField]
    private float speed;


    private void Start() 
    {
        Init();
    }

    void Update()
    {
        switch(id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                
                break;
        }
    }

    public void Init()
    {
        switch(id)
        {
            case 0:
                speed = 150;
                Batch();
                break;
            default:
                
                break;
        }
    }

    void Batch()
    {
        for(int i = 0; i < count; i++)
        {
            Transform bullet = GameManager.instance.GetPool.Get(prefabID).transform;
            bullet.parent = transform;
            bullet.GetComponent<Bullet>().Init(damage, -1);  // -1은 무한을 의미
        }
    }

}

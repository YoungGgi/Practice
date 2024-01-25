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

        if(Input.GetButtonDown("Jump"))
        {
            LevelUp(20, 7);
        }

    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage;
        this.count += count;

        if(id == 0)
        {
            Batch();
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
            Transform bullet;
            if(i < transform.childCount)
            {
                bullet = transform.GetChild(i);
            }
            else
            {
                bullet = GameManager.instance.GetPool.Get(prefabID).transform;
                bullet.parent = transform;
            }

            // 로컬포지션, 로컬회전값 초기화
            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            // 근거리 무기 회전
            Vector3 rotVec = Vector3.forward  * 360 * i / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World); // 자신의 위치에서 1.5 위로, 월드 방향으로 이동

            bullet.GetComponent<Bullet>().Init(damage, -1);  // -1은 무한을 의미
        }
    }

}

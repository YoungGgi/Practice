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

    float timer;
    Player player;

    private void Awake() 
    {
        player = GetComponentInParent<Player>();
    }


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
                timer += Time.deltaTime;

                if(timer > speed)
                {
                    timer = 0f;
                    Fire();
                }
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
                speed = 0.3f;
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

            bullet.GetComponent<Bullet>().Init(damage, -1, Vector3.zero);  // -1은 무한을 의미
        }
    }

    void Fire()
    {
        if(!player.Scan.Neartarget)
        {
            return;
        }

        Vector3 targetPos = player.Scan.Neartarget.position;
        Vector3 dir = targetPos - transform.position;
        dir = dir.normalized;

        // 근처에 몬스터가 있다면 원거리 공격 무기 위치 할당
        Transform bullet = GameManager.instance.GetPool.Get(2).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);

        bullet.GetComponent<Bullet>().Init(damage, count, dir);


    }

}

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

    public int GetWeaponID
    {
        get {return id;}
    }

    public float GetDamage
    {
        get {return damage;}
    }

    public int GetCount
    {
        get {return count;}
    }

    public float GetWeaponSpeed
    {
        get {return speed;}
        set {speed = value;}
    }

    float timer;
    Player player;

    private void Awake() 
    {
        player = GameManager.instance.GetPlayer;
    }

    void Update()
    {
        if(!GameManager.instance.GetIsLive)
          return;
        
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
            LevelUp(10, 7);
        }

    }

    public void LevelUp(float damage, int count)
    {
        this.damage = damage * Character.Damage;
        this.count += count;

        if(id == 0)
        {
            Batch();
        }

        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Init(ItemData data)
    {
        // 무기 레벨업 버튼 클릭 시 Weapon 정보를 할당
        name = $"Weapon {data.GetItemID}";
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.GetItemID;
        damage = data.GetBaseDamage * Character.Damage;
        count = data.GetBaseCount + Character.Count;

        for(int i = 0; i < GameManager.instance.GetPool.GetPrefabs.Length; i++)
        {
            if(data.GetProjectile == GameManager.instance.GetPool.GetPrefabs[i])
            {
                prefabID = i;
                break;
            }
        }
        
        switch(id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            default:
                speed = 0.4f* Character.WeaponRate;
                break;
        }

        // Hand
        Hand hand = player.GetHands[(int)data.itemType];
        hand.GetSprite.sprite = data.GetHand;
        hand.gameObject.SetActive(true);

        // BroadcastMessage() => 특정 메소드 호출을 모든 자식에게 방송하는 메소드
        // SendMessageOptions.DontRequireReceiver => 꼭 리시버가 필요하지 않음 => 장갑, 장화가 없을때 해당 메소드를 반드시 방송할 필요 없다?
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
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

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Melee);
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

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);


    }

}

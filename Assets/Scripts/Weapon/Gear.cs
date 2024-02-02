using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    [SerializeField]
    private ItemData.ItemType type;
    [SerializeField]
    private float rate;

    public void Init(ItemData data)
    {
        name = $"Gear {data.GetItemID}";
        transform.parent = GameManager.instance.GetPlayer.transform;
        transform.localPosition = Vector3.zero;

        type = data.itemType;
        rate = data.GetDamages[0];
        ApplyGear();

    }

    public void LevelUp(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch(type)
        {
            case ItemData.ItemType.Glove:
                RateUp();
                break;
            case ItemData.ItemType.Shoe:
                SpeedUp();
                break;
        }
    }

    // 장갑 전용, 연사속도 증가
    void RateUp()
    {
        Weapon[] weapons = transform.parent.GetComponentsInChildren<Weapon>();

        foreach(Weapon weapon in weapons)
        {
            switch(weapon.GetWeaponID)
            {
                case 0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.GetWeaponSpeed = speed + (speed * rate);
                    break;
                default:
                    speed = 0.5f * Character.WeaponSpeed;
                    weapon.GetWeaponSpeed = speed * (1f - rate);
                    break;
            }
        }
    }

    // 장화 전용, 이동속도 증가
    void SpeedUp()
    {
        float speed = 3 * Character.Speed;
        GameManager.instance.GetPlayer.GetPlayerSpeed = speed + speed * rate;
    }

}

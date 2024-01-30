using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Item : MonoBehaviour
{
    [SerializeField]
    private ItemData data;
    [SerializeField]
    private int level;
    [SerializeField]
    private Weapon weapon;
    [SerializeField]
    private Gear gear;

    Image icon;
    TextMeshProUGUI textLevel;

    private void Awake() {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.GetItemIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        textLevel = texts[0];
    }

    private void LateUpdate() 
    {
        textLevel.text = $"Lv.{level + 1}";
    }

    public void OnClick()
    {
        switch(data.itemType)
        {
            case ItemData.ItemType.Melee:
            case ItemData.ItemType.Range:
                if(level == 0)
                {
                    // 아이템이 없을 경우 GameObject 생성, Weapon 활성화
                    GameObject newWeapon = new GameObject();
                    weapon = newWeapon.AddComponent<Weapon>();
                    weapon.Init(data);
                }
                else
                {
                    // 아이템 활성화 후 레벨 증가 시 각 레벨에 따라 데미지(damages), 카운트(counts) 증가
                    float nextDamage = data.GetBaseDamage;
                    int nextCount = 0;

                    nextDamage += data.GetBaseDamage + data.GetDamages[level];
                    nextCount += data.GetCounts[level];

                    weapon.LevelUp(nextDamage, nextCount);

                }
                level++;
                break;
            case ItemData.ItemType.Glove:
            case ItemData.ItemType.Shoe:
                if(level == 0)
                {
                    // 아이템이 없을 경우 GameObject 생성, Gear 활성화
                    GameObject newGear = new GameObject();
                    gear = newGear.AddComponent<Gear>();
                    gear.Init(data);
                }
                else
                {
                    // 아이템 활성화 후 레벨 증가 시 각 레벨에 따라 연사속도, 이동속도(damages) 증가
                    float nextRate = data.GetDamages[level];
                    gear.LevelUp(nextRate);
                }
                level++;
                break;
            case ItemData.ItemType.Heal:
                GameManager.instance.GetHealth = GameManager.instance.GetMaxHealth;
                break;
        }

        

        if(level == data.GetDamages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }


}

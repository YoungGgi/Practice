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
                    // 아이템 활성화 후 레벨 증가 시 각 레벨에 따라 데미지, 카운트 증가
                    float nextDamage = data.GetBaseDamage;
                    int nextCount = 0;

                    nextDamage += data.GetBaseDamage + data.GetDamages[level];
                    nextCount += data.GetCounts[level];

                    weapon.LevelUp(nextDamage, nextCount);

                }
                break;
            case ItemData.ItemType.Glove:

                break;
            case ItemData.ItemType.Shoe:

                break;
            case ItemData.ItemType.Heal:

                break;
        }

        level++;

        if(level == data.GetDamages.Length)
        {
            GetComponent<Button>().interactable = false;
        }
    }


}

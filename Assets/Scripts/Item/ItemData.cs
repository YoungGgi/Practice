using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }
    
    
    [Header("# Main Info")]
    public ItemType itemType;
    [SerializeField]
    private int itemID;
    [SerializeField]
    private string itemName;
    [SerializeField]
    private string itemDesc;
    [SerializeField]
    private Sprite itemIcon;


    [Header("# Level Data")]
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private int baseCount;
    [SerializeField]
    private float[] damages;
    [SerializeField]
    private int[] counts;

    [Header("# Weapon")]
    [SerializeField]
    private GameObject projectile;

}

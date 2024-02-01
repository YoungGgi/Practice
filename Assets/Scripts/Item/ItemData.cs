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
    [TextArea]
    private string itemDesc;
    [SerializeField]
    private Sprite itemIcon;

    public int GetItemID
    {
        get {return itemID;}
    }

    public string GetItemName
    {
        get {return itemName;}
    }

    public string GetItemDesc
    {
        get {return itemDesc;}
    }


    public Sprite GetItemIcon
    {
        get { return itemIcon;}
    }


    [Header("# Level Data")]
    [SerializeField]
    private float baseDamage;
    [SerializeField]
    private int baseCount;
    [SerializeField]
    private float[] damages;
    [SerializeField]
    private int[] counts;

    public float GetBaseDamage
    {
        get {return baseDamage;}
    }

    public int GetBaseCount
    {
        get {return baseCount;}
    }

    public float[] GetDamages
    {
        get {return damages;}
    }

    public int[] GetCounts
    {
        get {return counts;}
    }

    [Header("# Weapon")]
    [SerializeField]
    private GameObject projectile;
    [SerializeField]
    private Sprite hand;

    public GameObject GetProjectile
    {
        get {return projectile;}
    }

    public Sprite GetHand
    {
        get {return hand;}
    }

}

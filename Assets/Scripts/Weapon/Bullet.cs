using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float damage;

    public float Damage
    {
        get {return damage; }
    }

    [SerializeField]
    private int per;

    public void Init(float damage, int per)
    {
        this.damage = damage;
        this.per = per;
    }
}

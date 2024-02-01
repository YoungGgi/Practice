using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private bool isLeft;
    [SerializeField]
    private SpriteRenderer spriter;

    public bool GetIsLeft
    {
        get {return isLeft;}
    }

    public SpriteRenderer GetSprite
    {
        get {return spriter;}
    }

    SpriteRenderer player;

    Vector3 rightPos = new Vector3(0.35f, -0.15f, 0);
    Vector3 rightPosReverse = new Vector3(-0.15f, -0.15f, 0);
    Quaternion leftRot = Quaternion.Euler(0, 0, -35);
    Quaternion leftRotReverse = Quaternion.Euler(0, 0, -135);

    private void Awake() 
    {
        player = GetComponentsInParent<SpriteRenderer>()[1];
    }

    private void LateUpdate() 
    {
        bool isReverse = player.flipX; // 플레이어가 왼쪽으로 이동 중일 때

        if(isLeft) // 근접 무기 들고 있는 손
        {
            transform.localRotation = isReverse ? leftRotReverse : leftRot;
            spriter.flipY = isReverse;
            spriter.sortingOrder = isReverse ? 4 : 6;
        }
        else // 원거리 무기 들고 있는 손
        {
            transform.localPosition = isReverse ? rightPosReverse : rightPos;
            spriter.flipX = isReverse;
            spriter.sortingOrder = isReverse ? 6 : 4;
        }
    }

}

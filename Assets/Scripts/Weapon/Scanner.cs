using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField]
    private float scanRange;
    [SerializeField]
    private LayerMask targetlayer;
    [SerializeField]
    private RaycastHit2D[] targets;
    [SerializeField]
    private Transform nearestTarget;

    public Transform Neartarget
    {
        get {return nearestTarget;}
    }


    // CircleCastAll = 원형의 캐스트를 쏘고 모든 결과를 반환하는 함수
    private void FixedUpdate() 
    {
        targets = Physics2D.CircleCastAll(transform.position, scanRange, Vector2.zero, 0, targetlayer);
        nearestTarget = GetNearest();
    }

    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;

        // 현재 위치와 타겟(몬스터)의 거리를 빼서 구함
        // 원거리 공격 인식 범위 설정 (자신을 기준으로 주변에 적이 있는지)
        foreach(RaycastHit2D target in targets)
        {
            Vector3 myPos = transform.position;
            Vector3 targetPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos, targetPos);

            if(curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }

        return result;
    }

}

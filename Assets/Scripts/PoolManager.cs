using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 필요 항목
    // 프리팹들을 보관할 변수들
    [SerializeField]
    private GameObject[] prefabs;

    public GameObject[] GetPrefabs
    {
        get {return prefabs;}
    }

    // 풀 담당을 하는 리스트들
    List<GameObject>[] pools;

    // 반복문을 통해 모든 풀 리스트 초기화
    private void Awake() 
    {
        pools = new List<GameObject>[prefabs.Length];

        for(int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }

    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        // 선택한 풀에 있는(비활성화된) 게임오브젝트 접근

        foreach(GameObject item in pools[index])
        {
            if(!item.activeSelf)
            {
                // 발견하면 select 변수에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        // 선택한 풀에 게임오브젝트가 없다면
        if(!select)
        {
            // 새롭게 생성해서 select 변수에 할당, pools 리스트에 등록
            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
            

        return select;
    }

}

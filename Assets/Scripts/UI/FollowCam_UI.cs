using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam_UI : MonoBehaviour
{
    RectTransform rect;

    private void Awake() 
    {
        rect = GetComponent<RectTransform>();
    }

    // WorldToScreenPoint = 월드 상의 오브젝트 위치를 스크린 좌표로 변환하는 메소드.
    private void FixedUpdate() 
    {
        // 체력바 UI의 rectTransform 위치를 스크린 좌표로 변환된 플레이어 위치로 할당.
        rect.position = Camera.main.WorldToScreenPoint(GameManager.instance.GetPlayer.transform.position);
    }

}

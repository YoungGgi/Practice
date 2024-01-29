using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public enum InfoType {Exp, Level, Kill, Time, Health}
    public InfoType type;

    TextMeshProUGUI myText;
    Slider mySlider;

    private void Awake() 
    {
        myText = GetComponent<TextMeshProUGUI>();
        mySlider = GetComponent<Slider>();
    }

    private void LateUpdate() 
    {
        switch(type)
        {
            case InfoType.Exp:
                float curExp = GameManager.instance.Get_Exp;
                float maxExp = GameManager.instance.GetNextExp[GameManager.instance.GetLevel];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:
                myText.text = string.Format("Lv.{0:F0}" , GameManager.instance.GetLevel);
                break;
            case InfoType.Kill:
                myText.text = string.Format("{0:F0}" , GameManager.instance.GetKill);
                break;
            case InfoType.Time:

                break;
            case InfoType.Health:

                break;
        }
    }

}

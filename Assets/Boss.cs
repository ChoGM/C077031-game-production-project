using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float bossHp = 100f; // Boss의 체력
    public Slider bossHpSlider; // Boss의 체력 슬라이더 UI

    // 체력 감소 함수
    public void DecreaseHp(float damage)
    {
        bossHp -= damage;
        if (bossHp < 0) bossHp = 0; // 체력이 0보다 적어지지 않도록 방지

        if (bossHpSlider != null)
        {
            bossHpSlider.value = bossHp; // 체력에 맞게 슬라이더 값 업데이트
        }

        Debug.Log("Boss Hp: " + bossHp);
    }

   
}

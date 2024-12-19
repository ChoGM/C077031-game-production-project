using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public float bossHp = 100f; // Boss�� ü��
    public Slider bossHpSlider; // Boss�� ü�� �����̴� UI

    // ü�� ���� �Լ�
    public void DecreaseHp(float damage)
    {
        bossHp -= damage;
        if (bossHp < 0) bossHp = 0; // ü���� 0���� �������� �ʵ��� ����

        if (bossHpSlider != null)
        {
            bossHpSlider.value = bossHp; // ü�¿� �°� �����̴� �� ������Ʈ
        }

        Debug.Log("Boss Hp: " + bossHp);
    }

   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillDamage : MonoBehaviour
{
    public float attackPower; // 스킬 데미지

    private Boss boss;

    void Update()
    {
        Destroy(gameObject, 1.1f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BOSS"))
        {
            boss = other.GetComponent<Boss>();
            if (boss != null)
            {
                boss.DecreaseHp(attackPower); // 보스 체력 감소
            }
        }
    }

    public void SetAttackPower(float power)
    {
        attackPower = power;
    }
}
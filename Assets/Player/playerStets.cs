using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStets : MonoBehaviour
{
    public float playerPowerSetting = 30f;

    public float attackPower = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomAttackPower();
    }

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 attackPower를 새로운 랜덤 값으로 설정
        SetRandomAttackPower();
    }

    void SetRandomAttackPower()
    {
        attackPower = Random.Range(playerPowerSetting, playerPowerSetting + 21);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Slider HpSlider;      // HP를 나타내는 슬라이더
    public Slider PotionSlider;  // 포션 쿨타임을 나타내는 슬라이더

    public float MaxHp = 100f;   // 최대 HP 값
    public float NowHp;     // 현재 HP 값
    private bool PotionCooldown = false;  // 포션 쿨타임 상태 확인
    private float PotionCooldownTime = 5f;  // 포션 쿨타임
    private float PotionCooldownTimer = 0f; // 포션 쿨타임 타이머

    public Text potionCountText; // 포션 수를 표시하는 텍스트 UI
    public int maxPotionCount = 5;  // 최대 포션 개수
    private int currentPotionCount; // 현재 포션 개수

    public Slider AroundBallSlider;
    public GameObject AroundBallObject; // 사용할 오브젝트 (구체)
    public bool AroundBallCooldown = true;  // 포션 쿨타임 상태 확인
    public float AroundBallTime = 1f;  // 구체 쿨타임 
    public float AroundBallTimer = 0f; // 구체 쿨타임 타이머

    // Start is called before the first frame update
    void Start()
    {
        NowHp = MaxHp;          // 처음에는 HP를 최대값으로 설정
        HpSlider.maxValue = MaxHp;  // HP 슬라이더의 최대값 설정
        HpSlider.value = NowHp; // HP 슬라이더 현재 값 설정
        PotionSlider.maxValue = 100; // 포션 슬라이더의 최대값 설정
        PotionSlider.value = 100;   // 처음에는 포션을 사용할 수 있게 100으로 설정

        currentPotionCount = maxPotionCount;  // 현재 포션 수는 최대값으로 설정
        UpdatePotionCountUI();               // 포션 수 UI 초기 업데이트

        AroundBallSlider.maxValue = 100;  // 구체(AroundBall) 슬라이더의 최대값 설정
        AroundBallSlider.value = 0;      // 구체는 시작 시 사용할 수 없으므로 슬라이더 값은 0
        AroundBallObject.SetActive(false);   // 처음에는 구체 오브젝트 비활성화

        NowHp = MaxHp;
        HpSlider.maxValue = MaxHp;
        HpSlider.value = NowHp;

        currentPotionCount = maxPotionCount;
        UpdatePotionCountUI();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.End) && !PotionCooldown && PotionSlider.value >= 100 && currentPotionCount > 0)
        {
            UsePotion();  // 포션 사용
        }

        if (Input.GetKeyDown(KeyCode.Space) && !AroundBallCooldown && AroundBallSlider.value >= 100)
        {
            UseAroundBall();  // 구체 사용
        }

        if (PotionCooldown)
        {
            PotionCooldownTimer += Time.deltaTime;
            PotionSlider.value = (PotionCooldownTimer / PotionCooldownTime) * 100; // 쿨타임 비율에 따라 슬라이더 값 갱신

            if (PotionCooldownTimer >= PotionCooldownTime) // 쿨타임이 끝났을 때
            {
                PotionCooldown = false;   // 쿨타임 상태 해제
                PotionSlider.value = 100;   // 포션 슬라이더 다시 채움
            }
        }

        if (AroundBallCooldown)
        {
            AroundBallTimer += Time.deltaTime; // 타이머 갱신
            AroundBallSlider.value = (AroundBallTimer / AroundBallTime) * 100; // 쿨타임 비율에 따라 슬라이더 값 갱신

            if (AroundBallTimer >= AroundBallTime) // 쿨타임이 끝났을 때
            {
                AroundBallCooldown = false;  // 쿨타임 상태 해제
                AroundBallSlider.value = 100;   // 구체 슬라이더 다시 채움
            }
        }

    }

    // 포션을 사용하여 HP를 회복
    void UsePotion()
    {
        NowHp = MaxHp;           // HP를 최대값으로 회복
        HpSlider.value = NowHp;  // HP 슬라이더 값 갱신
        PotionCooldown = true;     // 쿨타임 시작
        PotionCooldownTimer = 0f;    // 쿨타임 타이머 초기화
        PotionSlider.value = 0;      // 포션 슬라이더 0으로 설정

        currentPotionCount--;        // 포션 수 하나 감소
        UpdatePotionCountUI();       // 포션 수 UI 업데이트
    }

    // 구체(AroundBall)를 사용
    void UseAroundBall()
    {
        AroundBallObject.SetActive(true); // 구체 오브젝트 활성화     
        AroundBallTimer = 0f;             // 타이머 초기화
        AroundBallSlider.value = 0;       // 슬라이더 0으로 설정
    }

    public void UpdatePotionCountUI()
    {
        potionCountText.text = currentPotionCount.ToString(); // 포션 수를 텍스트로 표시
    }

}

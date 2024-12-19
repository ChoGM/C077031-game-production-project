using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    public Slider HpSlider;      // HP�� ��Ÿ���� �����̴�
    public Slider PotionSlider;  // ���� ��Ÿ���� ��Ÿ���� �����̴�

    public float MaxHp = 100f;   // �ִ� HP ��
    public float NowHp;     // ���� HP ��
    private bool PotionCooldown = false;  // ���� ��Ÿ�� ���� Ȯ��
    private float PotionCooldownTime = 5f;  // ���� ��Ÿ��
    private float PotionCooldownTimer = 0f; // ���� ��Ÿ�� Ÿ�̸�

    public Text potionCountText; // ���� ���� ǥ���ϴ� �ؽ�Ʈ UI
    public int maxPotionCount = 5;  // �ִ� ���� ����
    private int currentPotionCount; // ���� ���� ����

    public Slider AroundBallSlider;
    public GameObject AroundBallObject; // ����� ������Ʈ (��ü)
    public bool AroundBallCooldown = true;  // ���� ��Ÿ�� ���� Ȯ��
    public float AroundBallTime = 1f;  // ��ü ��Ÿ�� 
    public float AroundBallTimer = 0f; // ��ü ��Ÿ�� Ÿ�̸�

    // Start is called before the first frame update
    void Start()
    {
        NowHp = MaxHp;          // ó������ HP�� �ִ밪���� ����
        HpSlider.maxValue = MaxHp;  // HP �����̴��� �ִ밪 ����
        HpSlider.value = NowHp; // HP �����̴� ���� �� ����
        PotionSlider.maxValue = 100; // ���� �����̴��� �ִ밪 ����
        PotionSlider.value = 100;   // ó������ ������ ����� �� �ְ� 100���� ����

        currentPotionCount = maxPotionCount;  // ���� ���� ���� �ִ밪���� ����
        UpdatePotionCountUI();               // ���� �� UI �ʱ� ������Ʈ

        AroundBallSlider.maxValue = 100;  // ��ü(AroundBall) �����̴��� �ִ밪 ����
        AroundBallSlider.value = 0;      // ��ü�� ���� �� ����� �� �����Ƿ� �����̴� ���� 0
        AroundBallObject.SetActive(false);   // ó������ ��ü ������Ʈ ��Ȱ��ȭ

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
            UsePotion();  // ���� ���
        }

        if (Input.GetKeyDown(KeyCode.Space) && !AroundBallCooldown && AroundBallSlider.value >= 100)
        {
            UseAroundBall();  // ��ü ���
        }

        if (PotionCooldown)
        {
            PotionCooldownTimer += Time.deltaTime;
            PotionSlider.value = (PotionCooldownTimer / PotionCooldownTime) * 100; // ��Ÿ�� ������ ���� �����̴� �� ����

            if (PotionCooldownTimer >= PotionCooldownTime) // ��Ÿ���� ������ ��
            {
                PotionCooldown = false;   // ��Ÿ�� ���� ����
                PotionSlider.value = 100;   // ���� �����̴� �ٽ� ä��
            }
        }

        if (AroundBallCooldown)
        {
            AroundBallTimer += Time.deltaTime; // Ÿ�̸� ����
            AroundBallSlider.value = (AroundBallTimer / AroundBallTime) * 100; // ��Ÿ�� ������ ���� �����̴� �� ����

            if (AroundBallTimer >= AroundBallTime) // ��Ÿ���� ������ ��
            {
                AroundBallCooldown = false;  // ��Ÿ�� ���� ����
                AroundBallSlider.value = 100;   // ��ü �����̴� �ٽ� ä��
            }
        }

    }

    // ������ ����Ͽ� HP�� ȸ��
    void UsePotion()
    {
        NowHp = MaxHp;           // HP�� �ִ밪���� ȸ��
        HpSlider.value = NowHp;  // HP �����̴� �� ����
        PotionCooldown = true;     // ��Ÿ�� ����
        PotionCooldownTimer = 0f;    // ��Ÿ�� Ÿ�̸� �ʱ�ȭ
        PotionSlider.value = 0;      // ���� �����̴� 0���� ����

        currentPotionCount--;        // ���� �� �ϳ� ����
        UpdatePotionCountUI();       // ���� �� UI ������Ʈ
    }

    // ��ü(AroundBall)�� ���
    void UseAroundBall()
    {
        AroundBallObject.SetActive(true); // ��ü ������Ʈ Ȱ��ȭ     
        AroundBallTimer = 0f;             // Ÿ�̸� �ʱ�ȭ
        AroundBallSlider.value = 0;       // �����̴� 0���� ����
    }

    public void UpdatePotionCountUI()
    {
        potionCountText.text = currentPotionCount.ToString(); // ���� ���� �ؽ�Ʈ�� ǥ��
    }

}

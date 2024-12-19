using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindLaserAttack : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트 참조
    private PlayerHP playerHp; // 플레이어 체력 스크립트
    public Material attackMaterial; // 공격 시 적용될 Material
    private Material originalMaterial; // 원래의 Material 저장
    private PlayerMove playerMove; // 플레이어 체력 스크립트
    public BindLaserRotation bindLaserRotation;

    private float attackDelay = 1f; // 공격 지연 시간
    public float attackTime = 0f; // 공격 시간 누적
    public float vitalizeTime = 0f; // 공격 시간 누적
    public bool hasHitPlayer = false; // 플레이어가 맞았는지 여부
    private bool attackBool = false;
    private bool playerInRange = false; // 플레이어가 범위에 들어왔는지 여부
    private Renderer laserRenderer; // Laser 오브젝트의 렌더러

    private bool vitalizeBool;

    void Start()
    {
        playerHp = player.GetComponent<PlayerHP>();
        playerMove = player.GetComponent<PlayerMove>();
        laserRenderer = GetComponent<Renderer>(); // 이 오브젝트(Laser)의 Renderer 가져오기
        originalMaterial = laserRenderer.material; // 초기 Material 저장
    }

    // 활성화 할 때 초기화 해주는 코드
    void OnEnable()
    {
        attackTime = 0; // 공격 지연 시간 초기화
        hasHitPlayer = false; // 한 번만 감소하도록 설정
        attackBool = false;
        playerInRange = false; // 플레이어가 범위에 있는지 여부 초기화
        vitalizeTime = 1.5f; // 공격 시간 누적
        
        // 자신의 Material을 원래대로 돌려놓음
        if (laserRenderer != null)
        {
            laserRenderer.material = originalMaterial;
        }
    }

    void Update()
    {

        attackTime += Time.deltaTime; // 지연 시간 누적
        if (attackTime > attackDelay)
        {
            attackBool = true; // 1초 지연 후 공격 가능

            // 공격이 가능해지면 자신의 Material을 변경
            if (laserRenderer != null)
            {
                laserRenderer.material = attackMaterial;
            }
            if (attackTime > vitalizeTime)
            {
                vitalizeBool = false;
            }
        }

        // 공격 지연 시간 체크
        if (attackBool && playerInRange && !hasHitPlayer)
        {
            LaserAttackHit();
        }
    }

    void LaserAttackHit()
    {
        playerHp.HpSlider.value -= 10;
        playerMove.bindOn = true;
        hasHitPlayer = true; // 한 번만 공격되도록 설정
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            playerInRange = false;
        }
    }
}
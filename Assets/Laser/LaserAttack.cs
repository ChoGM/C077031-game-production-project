using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public LaserTarget laserTarget; // LaserTarget 스크립트 참조
    public GameObject player; // 플레이어 오브젝트 참조
    private PlayerHP playerHp; // 플레이어 체력 스크립트
    public Material attackMaterial; // 공격 시 적용될 Material
    private Material originalMaterial; // 원래의 Material 저장

    private float attackDelay = 1f; // 공격 지연 시간
    private float attackTime = 0f; // 공격 시간 누적
    public bool hasHitPlayer = false; // 플레이어가 맞았는지 여부
    private bool attackBool = false;
    private bool playerInRange = false; // 플레이어가 범위에 들어왔는지 여부
    private Renderer laserRenderer; // Laser 오브젝트의 렌더러

    void Start()
    {
        playerHp = player.GetComponent<PlayerHP>();
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

        // 자신의 Material을 원래대로 돌려놓음
        if (laserRenderer != null)
        {
            laserRenderer.material = originalMaterial;
        }
    }

    void Update()
    {
        // LaserTarget이 추적을 멈출 때까지 대기
        if (laserTarget.isTracking)
        {
            return; // 추적 중이면 아무것도 하지 않음
        }

        attackTime += Time.deltaTime; // 지연 시간 누적
        if (attackTime > attackDelay)
        {
            attackBool = true; // 2초 지연 후 공격 가능

            // 공격이 가능해지면 자신의 Material을 변경
            if (laserRenderer != null)
            {
                laserRenderer.material = attackMaterial;
            }
        }

        // 공격 지연 시간 체크, 범위 확인
        if (attackBool && playerInRange && !hasHitPlayer)
        {
            LaserAttackHit();
        }
    }

    void LaserAttackHit()
    {
        playerHp.HpSlider.value -= 30;
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
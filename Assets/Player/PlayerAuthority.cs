using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuthority : MonoBehaviour
{
    public bool authority1 = false;
    public bool authority2 = false;

    private Coroutine authority1Coroutine = null;
    private Coroutine authority2Coroutine = null;

    private PlayerHP playerHP; // PlayerHP 스크립트 참조

    private bool isCooldown = false;

    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isCooldown)
        {
            // 쿨다운 상태에서는 아무 동작도 하지 않음
            return;
        }

        if (other.CompareTag("AUTHORITY1"))
        {
            authority1 = true;
            Debug.Log("AUTHORITY1 활성화!");

            // 기존 코루틴 중단 후 새로 시작
            if (authority1Coroutine != null)
            {
                StopCoroutine(authority1Coroutine);
            }
            authority1Coroutine = StartCoroutine(ResetAuthorityAfterDelay(() => authority1 = false, "AUTHORITY1", 6f));
        }

        if (other.CompareTag("AUTHORITY2"))
        {
            authority2 = true;
            Debug.Log("AUTHORITY2 활성화!");

            // 기존 코루틴 중단 후 새로 시작
            if (authority2Coroutine != null)
            {
                StopCoroutine(authority2Coroutine);
            }
            authority2Coroutine = StartCoroutine(ResetAuthorityAfterDelay(() => authority2 = false, "AUTHORITY2", 6f));
        }

        // 두 bool 값이 모두 true라면 실행
        if (authority1 && authority2)
        {
            HandleBothAuthoritiesTrue();
        }
    }

    private void HandleBothAuthoritiesTrue()
    {
        Debug.Log("AUTHORITY1과 AUTHORITY2가 모두 활성화되었습니다!");

        // 실행 중인 코루틴 종료
        if (authority1Coroutine != null)
        {
            StopCoroutine(authority1Coroutine);
        }
        if (authority2Coroutine != null)
        {
            StopCoroutine(authority2Coroutine);
        }

        // authority 상태 초기화
        authority1 = false;
        authority2 = false;

        // 쿨다운 상태로 전환
        StartCoroutine(CooldownPeriod(2f));
    }

    IEnumerator ResetAuthorityAfterDelay(System.Action resetAction, string authorityName, float delay)
    {
        yield return new WaitForSeconds(delay);
        resetAction?.Invoke();
        Debug.Log($"{authorityName} 비활성화!");
    }

    IEnumerator CooldownPeriod(float cooldownTime)
    {
        isCooldown = true; // 쿨다운 활성화
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false; // 쿨다운 해제
        Debug.Log("쿨다운 종료: AUTHORITY1, AUTHORITY2 활성화 가능");
    }
}
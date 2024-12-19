using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAuthority : MonoBehaviour
{
    public bool authority1 = false;
    public bool authority2 = false;

    private Coroutine authority1Coroutine = null;
    private Coroutine authority2Coroutine = null;

    private PlayerHP playerHP; // PlayerHP ��ũ��Ʈ ����

    private bool isCooldown = false;

    void Start()
    {
        playerHP = GetComponent<PlayerHP>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (isCooldown)
        {
            // ��ٿ� ���¿����� �ƹ� ���۵� ���� ����
            return;
        }

        if (other.CompareTag("AUTHORITY1"))
        {
            authority1 = true;
            Debug.Log("AUTHORITY1 Ȱ��ȭ!");

            // ���� �ڷ�ƾ �ߴ� �� ���� ����
            if (authority1Coroutine != null)
            {
                StopCoroutine(authority1Coroutine);
            }
            authority1Coroutine = StartCoroutine(ResetAuthorityAfterDelay(() => authority1 = false, "AUTHORITY1", 6f));
        }

        if (other.CompareTag("AUTHORITY2"))
        {
            authority2 = true;
            Debug.Log("AUTHORITY2 Ȱ��ȭ!");

            // ���� �ڷ�ƾ �ߴ� �� ���� ����
            if (authority2Coroutine != null)
            {
                StopCoroutine(authority2Coroutine);
            }
            authority2Coroutine = StartCoroutine(ResetAuthorityAfterDelay(() => authority2 = false, "AUTHORITY2", 6f));
        }

        // �� bool ���� ��� true��� ����
        if (authority1 && authority2)
        {
            HandleBothAuthoritiesTrue();
        }
    }

    private void HandleBothAuthoritiesTrue()
    {
        Debug.Log("AUTHORITY1�� AUTHORITY2�� ��� Ȱ��ȭ�Ǿ����ϴ�!");

        // ���� ���� �ڷ�ƾ ����
        if (authority1Coroutine != null)
        {
            StopCoroutine(authority1Coroutine);
        }
        if (authority2Coroutine != null)
        {
            StopCoroutine(authority2Coroutine);
        }

        // authority ���� �ʱ�ȭ
        authority1 = false;
        authority2 = false;

        // ��ٿ� ���·� ��ȯ
        StartCoroutine(CooldownPeriod(2f));
    }

    IEnumerator ResetAuthorityAfterDelay(System.Action resetAction, string authorityName, float delay)
    {
        yield return new WaitForSeconds(delay);
        resetAction?.Invoke();
        Debug.Log($"{authorityName} ��Ȱ��ȭ!");
    }

    IEnumerator CooldownPeriod(float cooldownTime)
    {
        isCooldown = true; // ��ٿ� Ȱ��ȭ
        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false; // ��ٿ� ����
        Debug.Log("��ٿ� ����: AUTHORITY1, AUTHORITY2 Ȱ��ȭ ����");
    }
}
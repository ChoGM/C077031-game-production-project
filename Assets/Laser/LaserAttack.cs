using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAttack : MonoBehaviour
{
    public LaserTarget laserTarget; // LaserTarget ��ũ��Ʈ ����
    public GameObject player; // �÷��̾� ������Ʈ ����
    private PlayerHP playerHp; // �÷��̾� ü�� ��ũ��Ʈ
    public Material attackMaterial; // ���� �� ����� Material
    private Material originalMaterial; // ������ Material ����

    private float attackDelay = 1f; // ���� ���� �ð�
    private float attackTime = 0f; // ���� �ð� ����
    public bool hasHitPlayer = false; // �÷��̾ �¾Ҵ��� ����
    private bool attackBool = false;
    private bool playerInRange = false; // �÷��̾ ������ ���Դ��� ����
    private Renderer laserRenderer; // Laser ������Ʈ�� ������

    void Start()
    {
        playerHp = player.GetComponent<PlayerHP>();
        laserRenderer = GetComponent<Renderer>(); // �� ������Ʈ(Laser)�� Renderer ��������
        originalMaterial = laserRenderer.material; // �ʱ� Material ����
    }

    // Ȱ��ȭ �� �� �ʱ�ȭ ���ִ� �ڵ�
    void OnEnable()
    {
        attackTime = 0; // ���� ���� �ð� �ʱ�ȭ
        hasHitPlayer = false; // �� ���� �����ϵ��� ����
        attackBool = false;
        playerInRange = false; // �÷��̾ ������ �ִ��� ���� �ʱ�ȭ

        // �ڽ��� Material�� ������� ��������
        if (laserRenderer != null)
        {
            laserRenderer.material = originalMaterial;
        }
    }

    void Update()
    {
        // LaserTarget�� ������ ���� ������ ���
        if (laserTarget.isTracking)
        {
            return; // ���� ���̸� �ƹ��͵� ���� ����
        }

        attackTime += Time.deltaTime; // ���� �ð� ����
        if (attackTime > attackDelay)
        {
            attackBool = true; // 2�� ���� �� ���� ����

            // ������ ���������� �ڽ��� Material�� ����
            if (laserRenderer != null)
            {
                laserRenderer.material = attackMaterial;
            }
        }

        // ���� ���� �ð� üũ, ���� Ȯ��
        if (attackBool && playerInRange && !hasHitPlayer)
        {
            LaserAttackHit();
        }
    }

    void LaserAttackHit()
    {
        playerHp.HpSlider.value -= 30;
        hasHitPlayer = true; // �� ���� ���ݵǵ��� ����
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
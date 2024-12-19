using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindLaserAttack : MonoBehaviour
{
    public GameObject player; // �÷��̾� ������Ʈ ����
    private PlayerHP playerHp; // �÷��̾� ü�� ��ũ��Ʈ
    public Material attackMaterial; // ���� �� ����� Material
    private Material originalMaterial; // ������ Material ����
    private PlayerMove playerMove; // �÷��̾� ü�� ��ũ��Ʈ
    public BindLaserRotation bindLaserRotation;

    private float attackDelay = 1f; // ���� ���� �ð�
    public float attackTime = 0f; // ���� �ð� ����
    public float vitalizeTime = 0f; // ���� �ð� ����
    public bool hasHitPlayer = false; // �÷��̾ �¾Ҵ��� ����
    private bool attackBool = false;
    private bool playerInRange = false; // �÷��̾ ������ ���Դ��� ����
    private Renderer laserRenderer; // Laser ������Ʈ�� ������

    private bool vitalizeBool;

    void Start()
    {
        playerHp = player.GetComponent<PlayerHP>();
        playerMove = player.GetComponent<PlayerMove>();
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
        vitalizeTime = 1.5f; // ���� �ð� ����
        
        // �ڽ��� Material�� ������� ��������
        if (laserRenderer != null)
        {
            laserRenderer.material = originalMaterial;
        }
    }

    void Update()
    {

        attackTime += Time.deltaTime; // ���� �ð� ����
        if (attackTime > attackDelay)
        {
            attackBool = true; // 1�� ���� �� ���� ����

            // ������ ���������� �ڽ��� Material�� ����
            if (laserRenderer != null)
            {
                laserRenderer.material = attackMaterial;
            }
            if (attackTime > vitalizeTime)
            {
                vitalizeBool = false;
            }
        }

        // ���� ���� �ð� üũ
        if (attackBool && playerInRange && !hasHitPlayer)
        {
            LaserAttackHit();
        }
    }

    void LaserAttackHit()
    {
        playerHp.HpSlider.value -= 10;
        playerMove.bindOn = true;
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
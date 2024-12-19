using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindLaserBool : MonoBehaviour
{
    public BossAttack bossAttackScript; // BossAttack ��ũ��Ʈ�� ����
    public BindLaserAttack bindLaserAttackkScript; // BossAttack ��ũ��Ʈ�� ����
    public float vitalizeTime;

    private float elapsedTime = 0f; // ��� �ð� ���� ����
    private bool objectsActive = false; // �ڽ� ������Ʈ�� Ȱ��ȭ �Ǿ����� ����


    void Start()
    {
        vitalizeTime = 1.5f;
    }

    void Update()
    {
        // BossAttack ��ũ��Ʈ�� LaserBindBool�� true�� ���� ����
        if (bossAttackScript.LaserBindBool)
        {
            // �ڽ� ������Ʈ���� �迭�� ��������
            Transform[] childObjects = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                childObjects[i] = transform.GetChild(i);
                childObjects[i].gameObject.SetActive(true);
            }
            
            objectsActive = true;

            // �� �� ���� �� LaserBindBool�� false�� �����Ͽ� �ݺ� ���� ����
            bossAttackScript.LaserBindBool = false;
            elapsedTime = 0f;
        }

        if (objectsActive)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= vitalizeTime)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                objectsActive = false; // ���� �ʱ�ȭ
                elapsedTime = 0f; // ��� �ð� �ʱ�ȭ
            }
        }
    }
}
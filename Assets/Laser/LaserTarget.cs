using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    public Transform target; // �ٶ� Ÿ�� ������Ʈ
    public bool isTracking = true;
    private float trackingDuration = 3f; // ���� ���� �ð�
    private float trackingTime = 0f; // ���� �ð� ����

    public bool View3D = false;

    void Start()
    {
        trackingTime = 0f; // ���� �� ���� �ð� �ʱ�ȭ
    }

    void OnEnable()
    {
        isTracking = true;
        trackingDuration = 3f;
        trackingTime = 0f;
    }

    void Update()
    {
        if (isTracking && target != null)
        {
            Vector3 direction = target.position - transform.position;
            if (View3D == false)
            {
                direction.z = 0;
            }         

            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }

        // ���� �ð��� ������ �ð��� �����ϸ� ���� ����
        trackingTime += Time.deltaTime;
        if (trackingTime >= trackingDuration)
        {
            isTracking = false; // 3�� �� ���� ����
        }
    }

    public void ResetTracking()
    {
        isTracking = true; // �ٽ� Ÿ���� ����
        trackingTime = 0f; // ���� �ð� �ʱ�ȭ
    }
}
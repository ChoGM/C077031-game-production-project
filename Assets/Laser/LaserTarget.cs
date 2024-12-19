using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTarget : MonoBehaviour
{
    public Transform target; // 바라볼 타겟 오브젝트
    public bool isTracking = true;
    private float trackingDuration = 3f; // 추적 지속 시간
    private float trackingTime = 0f; // 추적 시간 누적

    public bool View3D = false;

    void Start()
    {
        trackingTime = 0f; // 시작 시 추적 시간 초기화
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

        // 추적 시간이 설정된 시간에 도달하면 추적 중지
        trackingTime += Time.deltaTime;
        if (trackingTime >= trackingDuration)
        {
            isTracking = false; // 3초 후 추적 멈춤
        }
    }

    public void ResetTracking()
    {
        isTracking = true; // 다시 타겟팅 시작
        trackingTime = 0f; // 추적 시간 초기화
    }
}
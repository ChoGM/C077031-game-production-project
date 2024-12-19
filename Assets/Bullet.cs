using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;  // 총알의 속도
    private Vector3 direction;       // 총알이 이동할 방향

    void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;

        direction = (transform.position - player.position).normalized;
    }

    void Update()
    {
        // 플레이어로부터 반대 방향으로 총알을 이동시킴
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);

        // 회전할 때 z값을 항상 0으로 고정
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = 0;  // z값 고정
        transform.rotation = Quaternion.Euler(rotation);

        Destroy(gameObject, 3.0f);
    }
}
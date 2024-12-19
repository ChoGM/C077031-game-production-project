using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed = 10f;  // �Ѿ��� �ӵ�
    private Vector3 direction;       // �Ѿ��� �̵��� ����

    void Start()
    {
        Transform player = GameObject.FindWithTag("Player").transform;

        direction = (transform.position - player.position).normalized;
    }

    void Update()
    {
        // �÷��̾�κ��� �ݴ� �������� �Ѿ��� �̵���Ŵ
        transform.Translate(direction * bulletSpeed * Time.deltaTime, Space.World);

        // ȸ���� �� z���� �׻� 0���� ����
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = 0;  // z�� ����
        transform.rotation = Quaternion.Euler(rotation);

        Destroy(gameObject, 3.0f);
    }
}
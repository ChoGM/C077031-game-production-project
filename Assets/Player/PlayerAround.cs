using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAround : MonoBehaviour
{
    public Transform target;  // ȸ���� ��� ������Ʈ (�߽�)
    public float rotationSpeed = 20f;  // ȸ�� �ӵ�
    public Vector3 rotationAxis = Vector3.up;  // ȸ�� �� 

    public GameObject Bullet;
    public Transform BulletSpawnPoint;

    private PlayerHP playerHP;

    void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
    }

    void Update()
    {
        // �߽� ������ ������Ʈ�� ȸ��
        transform.RotateAround(target.position, rotationAxis, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletInstance = Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

            // ������ �Ѿ��� z ���� �׻� 0���� ����
            Vector3 bulletRotation = bulletInstance.transform.rotation.eulerAngles;
            bulletRotation.z = 0;  // z�� ����
            bulletInstance.transform.rotation = Quaternion.Euler(bulletRotation);

            playerHP.AroundBallCooldown = true;        // ��Ÿ�� ����
            gameObject.SetActive(false);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAround : MonoBehaviour
{
    public Transform target;  // 회전할 대상 오브젝트 (중심)
    public float rotationSpeed = 20f;  // 회전 속도
    public Vector3 rotationAxis = Vector3.up;  // 회전 축 

    public GameObject Bullet;
    public Transform BulletSpawnPoint;

    private PlayerHP playerHP;

    void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();
    }

    void Update()
    {
        // 중심 주위로 오브젝트를 회전
        transform.RotateAround(target.position, rotationAxis, rotationSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bulletInstance = Instantiate(Bullet, BulletSpawnPoint.position, BulletSpawnPoint.rotation);

            // 생성된 총알의 z 값을 항상 0으로 고정
            Vector3 bulletRotation = bulletInstance.transform.rotation.eulerAngles;
            bulletRotation.z = 0;  // z값 고정
            bulletInstance.transform.rotation = Quaternion.Euler(bulletRotation);

            playerHP.AroundBallCooldown = true;        // 쿨타임 시작
            gameObject.SetActive(false);
        }
    }
}
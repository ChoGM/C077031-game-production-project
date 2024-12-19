using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // 이동 속도
    public float jumpForce = 5f; // 점프 힘
    private bool isGrounded; // 바닥에 있는지 확인하는 변수
    private int jumpCount = 0; // 현재 점프 횟수를 추적
    private const int maxJumpCount = 2; // 최대 점프 횟수
    private Rigidbody rb;

    public bool bindOn;
    private float bindOnTimer = 0f; // bindOn이 true일 때 시간을 추적할 변수
    private float bindOnDuration = 2f; // bindOn이 true로 유지될 시간

    public bool View3D = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        bindOn = false;
    }

    void Update()
    {
        if (bindOn)
        {
            bindOnTimer += Time.deltaTime;
            if (bindOnTimer >= bindOnDuration)
            {
                bindOn = false;
                bindOnTimer = 0f;
            }
        }

        if (bindOn == false)
        {
            Vector3 currentPosition = transform.position;

            Vector3 moveDirection = Vector3.zero;

            if (View3D == false)
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    moveDirection.x += 1f;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    moveDirection.x -= 1f;
                }
                currentPosition.z = 0; // z 값을 0으로 고정

                // View3D가 비활성화 상태일 때 회전값을 (0, 0, 0)으로 고정
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f); // x, z 회전 고정
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    moveDirection.x += 1f;
                }
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    moveDirection.x -= 1f;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    moveDirection.z += 1f;
                }
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    moveDirection.z -= 1f;
                }
            }

            // 이동 방향에 따른 플레이어의 정면 설정
            if (moveDirection.magnitude > 0)
            {
                transform.forward = moveDirection.normalized; // 이동 방향으로 정면 설정
            }

            currentPosition += moveDirection.normalized * moveSpeed * Time.deltaTime;

            transform.position = currentPosition;

            if (Input.GetKeyDown(KeyCode.LeftAlt) && jumpCount < maxJumpCount)
            {
                Jump();
            }

        }
    }

    void Jump()
    {
        if (bindOn == false)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpCount++;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GROUND"))
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("GROUND"))
        {
            isGrounded = false;
        }
    }

}

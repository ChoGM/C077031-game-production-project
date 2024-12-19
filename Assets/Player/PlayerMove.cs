using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f; // �̵� �ӵ�
    public float jumpForce = 5f; // ���� ��
    private bool isGrounded; // �ٴڿ� �ִ��� Ȯ���ϴ� ����
    private int jumpCount = 0; // ���� ���� Ƚ���� ����
    private const int maxJumpCount = 2; // �ִ� ���� Ƚ��
    private Rigidbody rb;

    public bool bindOn;
    private float bindOnTimer = 0f; // bindOn�� true�� �� �ð��� ������ ����
    private float bindOnDuration = 2f; // bindOn�� true�� ������ �ð�

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
                currentPosition.z = 0; // z ���� 0���� ����

                // View3D�� ��Ȱ��ȭ ������ �� ȸ������ (0, 0, 0)���� ����
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f); // x, z ȸ�� ����
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

            // �̵� ���⿡ ���� �÷��̾��� ���� ����
            if (moveDirection.magnitude > 0)
            {
                transform.forward = moveDirection.normalized; // �̵� �������� ���� ����
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

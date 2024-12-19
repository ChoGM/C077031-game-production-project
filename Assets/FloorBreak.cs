using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBreak : MonoBehaviour
{
    public GameObject[] objectsToSelect; // ������ ������Ʈ �迭
    public Color selectedColor = Color.red; // ������ ����
    public Color defaultColor = Color.white; // �⺻ ���� (��Ȱ��ȭ ����)
    private int currentIndex = -1; // ���� ���õ� ������Ʈ �ε���
    private float timer = 0f;
    private enum State { Idle, ChangingColor, Disabled, WaitingForReactivation }
    private State currentState = State.Idle; // ���� ����

    //chatGPT��� �ӽ��ڵ�
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // ���� ���õ� ������Ʈ�� ������ �� ������Ʈ ����
                if (currentIndex == -1)
                {
                    SelectNewObject();
                }
                break;

            case State.ChangingColor:
                // Ÿ�̸Ӹ� ������Ű�� ���� ���� ���� ����
                timer += Time.deltaTime;
                if (timer >= 3f)
                {
                    // 3�ʰ� ������ ������Ʈ ��Ȱ��ȭ
                    ResetObject(currentIndex);
                    currentState = State.Disabled; // ���� ����
                    timer = 0f; // Ÿ�̸� �ʱ�ȭ
                }
                break;

            case State.Disabled:
                // ��Ȱ��ȭ ���¿��� 3�� ��� �� ���ο� ������Ʈ ����
                timer += Time.deltaTime;
                if (timer >= 3f)
                {
                    currentState = State.WaitingForReactivation; // ��� ���·� ����
                    timer = 0f; // Ÿ�̸� �ʱ�ȭ
                }
                break;

            case State.WaitingForReactivation:
                // ��Ȱ��ȭ�� ������Ʈ�� 3�� �Ŀ� �ٽ� Ȱ��ȭ
                timer += Time.deltaTime;
                if (timer >= 3f)
                {
                    ReactivateObject(currentIndex); // ������Ʈ Ȱ��ȭ
                    currentIndex = -1; // ���� ������Ʈ ������ ���� �ε��� �ʱ�ȭ
                    currentState = State.Idle; // ���¸� Idle�� ����
                    timer = 0f; // Ÿ�̸� �ʱ�ȭ
                }
                break;
        }
    }

    void SelectNewObject()
    {
        // ���� ������Ʈ ��Ȱ��ȭ
        if (currentIndex >= 0 && currentIndex < objectsToSelect.Length)
        {
            ResetObject(currentIndex);
        }

        // ���ο� ������Ʈ ����
        currentIndex = Random.Range(0, objectsToSelect.Length);
        ChangeColor(currentIndex, selectedColor); // ���� ����

        // ������ ������Ʈ�� ������ �⺻ ������ ����
        for (int i = 0; i < objectsToSelect.Length; i++)
        {
            if (i != currentIndex)
            {
                ChangeColor(i, defaultColor);
            }
        }

        currentState = State.ChangingColor; // ���� ����
    }

    void ChangeColor(int index, Color color)
    {
        Renderer renderer = objectsToSelect[index].GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color; // ���� ����
        }
        objectsToSelect[index].SetActive(true); // ������Ʈ Ȱ��ȭ
    }

    void ResetObject(int index)
    {
        Renderer renderer = objectsToSelect[index].GetComponent<Renderer>();
        if (renderer != null)
        {
            // ���� �������� �ǵ�����
            renderer.material.color = defaultColor; // �⺻ �������� ���� (�Ͼ��)
        }
        objectsToSelect[index].SetActive(false); // ������Ʈ ��Ȱ��ȭ
    }

    void ReactivateObject(int index)
    {
        if (index >= 0 && index < objectsToSelect.Length)
        {
            objectsToSelect[index].SetActive(true); // ������Ʈ Ȱ��ȭ
            ChangeColor(index, selectedColor); // ���� �ٽ� ���� (���� ��������)
        }
    }
}
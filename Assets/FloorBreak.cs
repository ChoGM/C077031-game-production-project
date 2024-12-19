using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorBreak : MonoBehaviour
{
    public GameObject[] objectsToSelect; // 선택할 오브젝트 배열
    public Color selectedColor = Color.red; // 지정된 색상
    public Color defaultColor = Color.white; // 기본 색상 (비활성화 색상)
    private int currentIndex = -1; // 현재 선택된 오브젝트 인덱스
    private float timer = 0f;
    private enum State { Idle, ChangingColor, Disabled, WaitingForReactivation }
    private State currentState = State.Idle; // 현재 상태

    //chatGPT사용 임시코드
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // 현재 선택된 오브젝트가 없으면 새 오브젝트 선택
                if (currentIndex == -1)
                {
                    SelectNewObject();
                }
                break;

            case State.ChangingColor:
                // 타이머를 증가시키고 색상 변경 상태 유지
                timer += Time.deltaTime;
                if (timer >= 3f)
                {
                    // 3초가 지나면 오브젝트 비활성화
                    ResetObject(currentIndex);
                    currentState = State.Disabled; // 상태 변경
                    timer = 0f; // 타이머 초기화
                }
                break;

            case State.Disabled:
                // 비활성화 상태에서 3초 대기 후 새로운 오브젝트 선택
                timer += Time.deltaTime;
                if (timer >= 3f)
                {
                    currentState = State.WaitingForReactivation; // 대기 상태로 변경
                    timer = 0f; // 타이머 초기화
                }
                break;

            case State.WaitingForReactivation:
                // 비활성화된 오브젝트를 3초 후에 다시 활성화
                timer += Time.deltaTime;
                if (timer >= 3f)
                {
                    ReactivateObject(currentIndex); // 오브젝트 활성화
                    currentIndex = -1; // 다음 오브젝트 선택을 위해 인덱스 초기화
                    currentState = State.Idle; // 상태를 Idle로 변경
                    timer = 0f; // 타이머 초기화
                }
                break;
        }
    }

    void SelectNewObject()
    {
        // 이전 오브젝트 비활성화
        if (currentIndex >= 0 && currentIndex < objectsToSelect.Length)
        {
            ResetObject(currentIndex);
        }

        // 새로운 오브젝트 선택
        currentIndex = Random.Range(0, objectsToSelect.Length);
        ChangeColor(currentIndex, selectedColor); // 색상 변경

        // 나머지 오브젝트의 색상을 기본 색으로 변경
        for (int i = 0; i < objectsToSelect.Length; i++)
        {
            if (i != currentIndex)
            {
                ChangeColor(i, defaultColor);
            }
        }

        currentState = State.ChangingColor; // 상태 변경
    }

    void ChangeColor(int index, Color color)
    {
        Renderer renderer = objectsToSelect[index].GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color; // 색상 변경
        }
        objectsToSelect[index].SetActive(true); // 오브젝트 활성화
    }

    void ResetObject(int index)
    {
        Renderer renderer = objectsToSelect[index].GetComponent<Renderer>();
        if (renderer != null)
        {
            // 원래 색상으로 되돌리기
            renderer.material.color = defaultColor; // 기본 색상으로 설정 (하얀색)
        }
        objectsToSelect[index].SetActive(false); // 오브젝트 비활성화
    }

    void ReactivateObject(int index)
    {
        if (index >= 0 && index < objectsToSelect.Length)
        {
            objectsToSelect[index].SetActive(true); // 오브젝트 활성화
            ChangeColor(index, selectedColor); // 색상 다시 변경 (선택 색상으로)
        }
    }
}
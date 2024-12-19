using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindLaserBool : MonoBehaviour
{
    public BossAttack bossAttackScript; // BossAttack 스크립트를 참조
    public BindLaserAttack bindLaserAttackkScript; // BossAttack 스크립트를 참조
    public float vitalizeTime;

    private float elapsedTime = 0f; // 경과 시간 추적 변수
    private bool objectsActive = false; // 자식 오브젝트가 활성화 되었는지 여부


    void Start()
    {
        vitalizeTime = 1.5f;
    }

    void Update()
    {
        // BossAttack 스크립트의 LaserBindBool이 true일 때만 실행
        if (bossAttackScript.LaserBindBool)
        {
            // 자식 오브젝트들을 배열로 가져오기
            Transform[] childObjects = new Transform[transform.childCount];
            for (int i = 0; i < transform.childCount; i++)
            {
                childObjects[i] = transform.GetChild(i);
                childObjects[i].gameObject.SetActive(true);
            }
            
            objectsActive = true;

            // 한 번 실행 후 LaserBindBool을 false로 변경하여 반복 실행 방지
            bossAttackScript.LaserBindBool = false;
            elapsedTime = 0f;
        }

        if (objectsActive)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= vitalizeTime)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    transform.GetChild(i).gameObject.SetActive(false);
                }
                objectsActive = false; // 상태 초기화
                elapsedTime = 0f; // 경과 시간 초기화
            }
        }
    }
}
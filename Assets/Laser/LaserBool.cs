using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBool : MonoBehaviour
{
    public GameObject[] objectsToActivate; // 활성화할 오브젝트 배열

    private void Start()
    {
        StartCoroutine(ActivateObjects());
    }

    private IEnumerator ActivateObjects()
    {
        while (true)
        {    
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(true);

                // LaserTarget이 활성화된 오브젝트인지 확인
                LaserTarget laserTarget = obj.GetComponent<LaserTarget>();
                if (laserTarget != null)
                {
                    laserTarget.ResetTracking(); // 레이저 타겟 초기화
                }
            }

            // 5초 대기
            yield return new WaitForSeconds(4.5f);

            // 오브젝트 비활성화
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(false);
            }

            // 5초 대기
            yield return new WaitForSeconds(5f);
        }
    }
}
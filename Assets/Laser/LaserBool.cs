using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBool : MonoBehaviour
{
    public GameObject[] objectsToActivate; // Ȱ��ȭ�� ������Ʈ �迭

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

                // LaserTarget�� Ȱ��ȭ�� ������Ʈ���� Ȯ��
                LaserTarget laserTarget = obj.GetComponent<LaserTarget>();
                if (laserTarget != null)
                {
                    laserTarget.ResetTracking(); // ������ Ÿ�� �ʱ�ȭ
                }
            }

            // 5�� ���
            yield return new WaitForSeconds(4.5f);

            // ������Ʈ ��Ȱ��ȭ
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(false);
            }

            // 5�� ���
            yield return new WaitForSeconds(5f);
        }
    }
}
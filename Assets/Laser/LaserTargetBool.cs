using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTargetBool : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // �浹�� ������Ʈ�� �±װ� BULLET�� ���
        if (other.CompareTag("BULLET"))
        {
            gameObject.SetActive(false); // �� ������Ʈ�� ��Ȱ��ȭ
        }
    }

}

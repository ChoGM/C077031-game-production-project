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
        // 충돌한 오브젝트의 태그가 BULLET일 경우
        if (other.CompareTag("BULLET"))
        {
            gameObject.SetActive(false); // 이 오브젝트를 비활성화
        }
    }

}

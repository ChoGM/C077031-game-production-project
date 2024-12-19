using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindLaserRotation : MonoBehaviour
{
    public bool View3D;
    // Start is called before the first frame update
    void Start()
    {

    }
  
    void OnEnable()
    {
        // X축의 회전 값을 75에서 105 사이의 랜덤 값으로 설정
        float randomXRotation = Random.Range(75f, 105f);
        float randomYRotation = Random.Range(75f, 105f);

        // 현재 회전 값을 가져오고 X축을 랜덤 값으로 설정
        Vector3 newRotation = transform.eulerAngles;
        newRotation.x = randomXRotation;

        if (View3D == true)
        {
            newRotation.y = randomYRotation;
        }
        else
        {
            newRotation.y = -90.0f;
        }
        newRotation.z = -90.0f;

        // 오브젝트에 새로운 회전 값 적용
        transform.eulerAngles = newRotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

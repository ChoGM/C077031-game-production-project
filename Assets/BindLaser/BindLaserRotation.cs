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
        // X���� ȸ�� ���� 75���� 105 ������ ���� ������ ����
        float randomXRotation = Random.Range(75f, 105f);
        float randomYRotation = Random.Range(75f, 105f);

        // ���� ȸ�� ���� �������� X���� ���� ������ ����
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

        // ������Ʈ�� ���ο� ȸ�� �� ����
        transform.eulerAngles = newRotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

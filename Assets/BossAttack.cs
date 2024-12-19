using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public bool LaserBindBool;
    public bool BindAfterAttack;

    private float timer = 0f;
    private float randomTime;
    private float laserDelay = 2f;
    private float laserTimer = 0f;

    public bool View3D;

    public GameObject bindAfterAttack;
    public GameObject bindAfterAttack3D;
    // Start is called before the first frame update
    void Start()
    {
        LaserBindBool = false;
        BindAfterAttack = false;
        randomTime = Random.Range(5f, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //laser공격
        if (timer >= randomTime)
        {
            LaserBindBool = true;
            BindAfterAttack = true;

            timer = 0f;
            randomTime = Random.Range(5f, 10f);
        }

        //레이저 바인드 공격
        if (BindAfterAttack)
        {
            laserTimer += Time.deltaTime;

            if (laserTimer >= laserDelay)
            {
                if (View3D == true)
                {                   
                    Instantiate(bindAfterAttack3D, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(bindAfterAttack, transform.position, Quaternion.identity);
                }

                laserTimer = 0f;
                BindAfterAttack = false;
            }
        }
    }
}

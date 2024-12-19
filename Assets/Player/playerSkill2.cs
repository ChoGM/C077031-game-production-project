using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkill2 : MonoBehaviour
{
    public GameObject prefabToSpawn; // ������ ������
    public Transform spawnPoint; // ������ ���� ��ġ
    private playerStets playerStats;

    private SKillDamage sKillDamage;

    void Start()
    {
        playerStats = GetComponent<playerStets>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            SpawnAtChildPosition();
        }
    }

    void SpawnAtChildPosition()
    {
        if (prefabToSpawn != null && spawnPoint != null)
        {
            Vector3 spawnPosition = spawnPoint.position;
            Quaternion spawnRotation = spawnPoint.rotation;

            GameObject skillInstance = Instantiate(prefabToSpawn, spawnPosition, spawnRotation);

            SKillDamage sKillDamage = skillInstance.GetComponent<SKillDamage>();
            if (sKillDamage != null && playerStats != null)
            {
                sKillDamage.SetAttackPower(playerStats.attackPower);
            }
        }
        else
        {
            Debug.LogWarning("PrefabToSpawn �Ǵ� SpawnPoint�� �������� �ʾҽ��ϴ�!");
        }
    }
}
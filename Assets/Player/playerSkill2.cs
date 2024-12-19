using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSkill2 : MonoBehaviour
{
    public GameObject prefabToSpawn; // 생성할 프리팹
    public Transform spawnPoint; // 프리팹 생성 위치
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
            Debug.LogWarning("PrefabToSpawn 또는 SpawnPoint가 설정되지 않았습니다!");
        }
    }
}
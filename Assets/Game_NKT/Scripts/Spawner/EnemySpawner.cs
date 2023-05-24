using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : Spawner
{
    private static EnemySpawner instance;
    public static EnemySpawner Instance { get => instance; }

    private int numberSpawn;

    [SerializeField] private Enemy enemyPrfab;

    [SerializeField] protected List<Transform> spawnPos = new List<Transform>();

    [SerializeField] protected List<Transform> reSpawnPos = new List<Transform>();

    private void Awake()
    {
        instance = this;

        this.numberSpawn = 0;
    }
    public Vector3 GetClosestPointOnNavmesh(Vector3 pos)
    {
        NavMeshHit myNavHit;
        if (NavMesh.SamplePosition(pos, out myNavHit, 100, -1))
        {
            return myNavHit.position;
        }
        return pos;
    }
    public override Transform SpawnPos()
    {
        if (spawnPos.Count < numberSpawn + 1)
        {
            return null;
        }
        return spawnPos[numberSpawn];
    }

    public Transform ReSpawnPos()
    {
        if (reSpawnPos.Count < numberSpawn + 1)
        {
            return null;
        }
        return reSpawnPos[numberSpawn];
    }

    protected override void OnSpawn()
    {
        base.OnSpawn();
        for (int i = 0; i < spawnPos.Count; i++)
        {
            Enemy enemyPool = SimplePool.Spawn<Enemy>(enemyPrfab, GetClosestPointOnNavmesh(SpawnPos().position), SpawnPos().rotation);

            enemyPool.OnInit();
            numberSpawn++;
        }

        this.numberSpawn = 0;

    }

    public void SpawnEnemies()
    {
        this.OnSpawn();
    }

    public void ReSpawn(int numberSpawn)
    {
        for (int i = 0; i < numberSpawn - 1; i++)
        {
            Enemy enemyPool = SimplePool.Spawn<Enemy>(enemyPrfab, GetClosestPointOnNavmesh(ReSpawnPos().position), ReSpawnPos().rotation);
            enemyPool.OnInit();
            this.numberSpawn++;
        }

        this.numberSpawn = 0;
    }


}

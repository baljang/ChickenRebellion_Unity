using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpawningPool : MonoBehaviour
{
    [SerializeField]
    Vector3 _spawnPos;

    [SerializeField]
    float _spawnRadius = 15.0f;

    public void AddMonsterCount(int value) 
    {
        Managers.Game.SaveData.remainEnemy += value;
    }

    public void AddPlayerCount(int value)
    {
        Managers.Game.SaveData.remainChicken += value;
    }

    void Start()
    {
        if (Managers.Game.OnMonsterSpawnEvent == null)
        {
            Managers.Game.OnMonsterSpawnEvent += AddMonsterCount;
        }
        if (Managers.Game.OnPlayerSpawnEvent == null)
        {
            Managers.Game.OnPlayerSpawnEvent += AddPlayerCount;
        }

    }

    void Update()
    {
    }

    public void Spawn(GameObject center, int count, Define.WorldObject kind, string name, int level)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Managers.Game.Spawn(kind, name);
            NavMeshAgent nma = obj.GetOrAddComponent<NavMeshAgent>();

            Vector3 randPos;
            while (true)
            {
                _spawnPos = center.transform.position;
                _spawnPos.y = 0; 

                Vector3 randDir = Random.insideUnitSphere * Random.Range(0, _spawnRadius);
                randDir.y = 0;

                if(kind == Define.WorldObject.Monster)
                    randPos = _spawnPos + randDir + new Vector3(-10, 0, 0);
                else if(kind == Define.WorldObject.Player)
                    randPos = _spawnPos + randDir + new Vector3(0, 0, -5);
                else 
                    randPos = _spawnPos + randDir + new Vector3(0, 0, -5);

                // 갈 수 있나
                NavMeshPath path = new NavMeshPath();
                if (nma.CalculatePath(randPos, path))
                    break;
            }
            obj.transform.position = randPos;
            obj.GetComponent<Stat>().SetStat(level); 


        }
    }
    
}

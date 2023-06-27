using System.Collections.Generic;
using UnityEngine;

public class LockManager : MonoBehaviour
{
    List<GameObject> _locks = new List<GameObject>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Lock"); 
        if(root == null)
        {
            root = new GameObject { name = "@LockManager" };
            Object.DontDestroyOnLoad(root); 
        }

        for (int i = 0; i < 5; i++)
        {
            GameObject go = Managers.Resource.Instantiate($"Lock");
            _locks.Add(go);
            go.transform.SetParent(root.transform);        
        }

        _locks[0].transform.position = new Vector3((float)15.28, (float)0.72, (float)3.39); 
        _locks[1].transform.position = new Vector3((float)15.46, (float)0.72, (float)-13.61);
        _locks[2].transform.position = new Vector3((float)17.42, (float)0.72, (float)-23.74);
        _locks[3].transform.position = new Vector3((float)16.6, (float)0.72, (float)-38.37);
        _locks[4].transform.position = new Vector3((float)17.9, (float)0.72, (float)-47.62);

        _locks[0].GetComponent<Stat>().Level = 1; 
        _locks[1].GetComponent<Stat>().Level = 2; 
        _locks[2].GetComponent<Stat>().Level = 3; 
        _locks[3].GetComponent<Stat>().Level = 4; 
        _locks[4].GetComponent<Stat>().Level = 7; 

        for(int i=0; i<5; i++)
        {
            _locks[i].GetComponent<Lock>()._keyNum = i;
        }
        _locks[0].GetComponent<Lock>()._kindEnemy = "Dog";
        _locks[1].GetComponent<Lock>()._kindEnemy = "FreshMan"; 
        _locks[2].GetComponent<Lock>()._kindEnemy = "ElderGuy"; 
        _locks[3].GetComponent<Lock>()._kindEnemy = "Police"; 
        _locks[4].GetComponent<Lock>()._kindEnemy = "Ceo";

        _locks[0].GetComponent<Lock>()._level = 1; 
        _locks[1].GetComponent<Lock>()._level = 2;
        _locks[2].GetComponent<Lock>()._level = 3;
        _locks[3].GetComponent<Lock>()._level = 4;
        _locks[4].GetComponent<Lock>()._level = 7;

        _locks[4].GetComponent<Lock>()._enemyNum = 1; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReleaseLocks()
    {
        foreach (GameObject lockObject in _locks)
        {
            Destroy(lockObject);
        }

        _locks.Clear();

        for(int i=0; i<5; i++)
        {
            Managers.Game.SaveData.keys[i] = false; 
        }
    }
}

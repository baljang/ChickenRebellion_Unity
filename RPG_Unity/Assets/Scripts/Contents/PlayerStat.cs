using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField]
    protected int _exp;
    [SerializeField]
    protected int _gold;

    public int Exp 
    { 
        get { return _exp; } 
        set 
        { 
            _exp = value;
            // 레벨업 체크

            int level = 3; 
            while(true)
            {
                Data.Stat stat;
                if (Managers.Data.StatDict.TryGetValue(level + 1, out stat) == false)
                    break;
                if (_exp < stat.totalExp)
                    break;
                level++; 
            }

            if(level != Level)
            {
                Debug.Log("Level Up!"); 
                Level = level;
                SetStat(Level); 
            }
        } 
    }
    public int Gold { get { return _gold; } set { _gold = value; } }

    private void Start()
    {
        if (seted == false)
        {
            _level = 1;
            _exp = 0;
            _defense = 5;
            _moveSpeed = 10.0f;
            _gold = 0;

            SetStat(_level);
        }
    }

    protected override void OnDead(Stat attacker)
    {
        Debug.Log("Player Dead");
        Managers.Game.Despawn(gameObject);
    }
}

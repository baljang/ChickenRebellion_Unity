using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    protected bool seted = false; 

    [SerializeField]
    protected int _level;
    [SerializeField]
    protected int _hp;
    [SerializeField]
    protected int _maxHp;
    [SerializeField]
    protected int _attack;
    [SerializeField]
    protected int _defense;
    [SerializeField]
    protected float _moveSpeed;
    [SerializeField]

    public int Level { get { return _level; } set { _level = value; } }
    public int Hp { get { return _hp; } set { _hp = value; } }
    public int MaxHp { get { return _maxHp; } set { _maxHp = value; } }
    public int Attack { get { return _attack; } set { _attack = value; } }
    public int Defence { get { return _defense; } set { _defense = value; } }
    public float MoveSpeed { get { return _moveSpeed; } set { _moveSpeed = value; } }

    private void Start()
    {
        if (seted == false)
        {
            if(_level ==0)
            _level = 1;

            if (GetComponent<Lock>() == true)
            {
                _hp = 1000;
                _maxHp = 1000;
            }
            else
            {
                _hp = 100;
                _maxHp = 100;
            }

            _attack = 10;
            _defense = 5;
        }
        _moveSpeed = 15.0f;


    }

    public void SetStat(int level)
    {
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        Data.Stat stat = dict[level];
        _level = stat.level; 
        _hp = stat.maxHp;
        _maxHp = stat.maxHp;
        _attack = stat.attack;
        _defense = stat.defence;
        _moveSpeed = stat.moveSpeed; 

        seted = true;
    }

    public void OnAttacked(Stat attacker)
    {
        int damage = Mathf.Max(0, attacker.Attack - Defence);
        Hp -= damage;
        if (Hp <= 0)
        {
            Hp = 0;
            OnDead(attacker);
        }  
    }

    protected virtual void OnDead(Stat attacker)
    {
        PlayerStat playerStat = attacker as PlayerStat;
        if (playerStat != null)
        {
            playerStat.Exp += 5; 
        }

        if (GetComponent<Lock>() == false)
            Managers.Game.Despawn(gameObject);  
    }
}

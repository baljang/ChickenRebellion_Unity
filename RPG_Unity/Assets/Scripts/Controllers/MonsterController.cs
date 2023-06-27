using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : BaseController
{
    Stat _stat;

    [SerializeField]
    float _scanRange = 100;

    [SerializeField]
    float _attackRange = 2; 

    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Monster; 
        _stat = gameObject.GetComponent<Stat>();

        if(gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform);
    }

    protected override void UpdateIdle()
    {
        if (this.gameObject.GetComponent<Lock>())
            return; 

        // TODO : 매니저가 생기면 옮기자
        List<GameObject> players = Managers.Game.GetPlayers();
        if (players == null)
            return;

        GameObject closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (GameObject player in players)
        {
            float distance = (player.transform.position - transform.position).magnitude;
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        if (closestPlayer != null && closestDistance <= _scanRange)
        {
            _lockTarget = closestPlayer;
            State = Define.State.Moving;
        }
    }

    protected override void UpdateMoving()
    {
        // 플레이어가 내 사정거리 보다 가까우면 공격
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= _attackRange)
            {
                NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
                nma.SetDestination(transform.position);
                State = Define.State.Skill;
                return;
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            State = Define.State.Idle;
        }
        else
        {
            NavMeshAgent nma = gameObject.GetOrAddComponent<NavMeshAgent>();
            nma.SetDestination(_destPos);
            nma.speed = _stat.MoveSpeed; 

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if (_lockTarget != null)
        {
            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
    }

    void OnHitEvent()
    {
        if(_lockTarget != null)
        {
            // 체력
            Stat targetStat = _lockTarget.GetComponent<Stat>();
            targetStat.OnAttacked(_stat); 

            if(targetStat.Hp > 0)
            {
                float distance = (_lockTarget.transform.position - transform.position).magnitude;
                if (distance <= _attackRange)
                    State = Define.State.Skill;
                else
                    State = Define.State.Moving; 
            }
            else
            {
                State = Define.State.Idle;
            }
        }
        else
        {
            State = Define.State.Idle;
        }
    }
}

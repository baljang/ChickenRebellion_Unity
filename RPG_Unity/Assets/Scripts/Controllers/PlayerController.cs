using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : BaseController
{
    int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    [SerializeField]
    float _scanRange = 30;

    [SerializeField]
    float _attackRange = 2;

    PlayerStat _stat;
    bool _stopSkill = false;

    void AutoIdleToMoving()
    {
        HashSet<GameObject> monsters = Managers.Game.GetMonsters(); // GetMonsters()는 범위 내의 모든 몬스터를 반환합니다. 필요에 따라 구현해야 합니다.
        
        if (monsters != null)
        {
            GameObject closestMonster = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject monster in monsters)
            {
                if (monster == null)
                    continue; 

                float distance = (monster.transform.position - transform.position).magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestMonster = monster;
                }
            }

            // 만약 범위 내에 몬스터가 있다면 공격 상태로 전환
            if (closestMonster != null && closestDistance <= _scanRange)
            {
                _lockTarget = closestMonster;
                State = Define.State.Moving;
                return;
            }
        }
    }

    void FindLockTarget()
    {
        HashSet<GameObject> monsters = Managers.Game.GetMonsters(); // GetMonsters()는 범위 내의 모든 몬스터를 반환합니다. 필요에 따라 구현해야 합니다.

        if (monsters != null)
        {
            GameObject closestMonster = null;
            float closestDistance = float.MaxValue;

            foreach (GameObject monster in monsters)
            {
                if (monster == null)
                    continue;

                float distance = (monster.transform.position - transform.position).magnitude;
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestMonster = monster;
                }
            }

            if (closestMonster != null && closestDistance <= _scanRange)
            {
                _lockTarget = closestMonster;
            }
        }
    }


    public override void Init()
    {
        WorldObjectType = Define.WorldObject.Player;
        _stat = gameObject.GetComponent<PlayerStat>(); 
        Managers.Input.MouseAction -= OnMouseEvent; 
        Managers.Input.MouseAction += OnMouseEvent;

        if (gameObject.GetComponentInChildren<UI_HPBar>() == null)
            Managers.UI.MakeWorldSpaceUI<UI_HPBar>(transform); 
    }

    protected override void UpdateIdle()
    {
        if (Managers.Game.autoMode)
            AutoIdleToMoving();
    }

    protected override void UpdateMoving()
    {
        if (Managers.Game.autoMode && _lockTarget == null)
            FindLockTarget(); 

        if (Managers.Game.autoMode && _lockTarget != null)
        {
            if(_lockTarget.GetComponent<Stat>().Hp == 0)
                FindLockTarget();

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

        // 몬스터가 내 사정거리 보다 가까우면 공격
        if (_lockTarget != null)
        {
            _destPos = _lockTarget.transform.position;
            float distance = (_destPos - transform.position).magnitude; 
            if(distance <= 1)
            {
                State = Define.State.Skill;
                return; 
            }
        }

        // 이동
        Vector3 dir = _destPos - transform.position;
        dir.y = 0; 

        if (dir.magnitude < 0.5f)
        {
            State = Define.State.Idle;
        }
        else
        {
            Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green); 
            if(Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
            {
                if(Input.GetMouseButton(0) == false)
                    State = Define.State.Idle; 
                return; 
            }
            
            float moveDist = Mathf.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);
            transform.position += dir.normalized * moveDist; 
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    protected override void UpdateSkill()
    {
        if (Managers.Game.autoMode && _lockTarget == null)
            FindLockTarget();

        if (Managers.Game.autoMode && _lockTarget != null)
        {
            if (_lockTarget.GetComponent<Stat>().Hp == 0)
                FindLockTarget();

            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
            return; 
        }
       
        if (_lockTarget != null)
        {
            Lock go = _lockTarget.GetComponent<Lock>();
            if (go != null)
            {
                if (go._lockVisual.activeSelf != true)
                    State = Define.State.Idle;
                return; 
            }

            Vector3 dir = _lockTarget.transform.position - transform.position;
            Quaternion quat = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, quat, 20 * Time.deltaTime);
        }
        else
            State = Define.State.Idle; 
    }

    void OnHitEvent()
    {
        if (Managers.Game.autoMode)
        {
            if (_lockTarget != null)
            {
                // 체력
                Stat targetStat = _lockTarget.GetComponent<Stat>();
                targetStat.OnAttacked(_stat);

                if (targetStat.Hp > 0)
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
        else
        {
            Debug.Log("OnHitEvent");

            if (_lockTarget != null)
            {
                Stat targetStat = _lockTarget.GetComponent<Stat>();
                targetStat.OnAttacked(_stat);
            }

            // TODO
            if (_stopSkill)
            {
                State = Define.State.Idle;
            }
            else
            {
                State = Define.State.Skill;
            }
        }
    }
       
    void OnMouseEvent(Define.MouseEvent evt)
    {        
        if (!this.gameObject.GetComponent<UnitController>().unitMarker.activeSelf)
            return; 

        switch(State)
        {
            case Define.State.Idle:
                OnMouseEvent_IdleRun(evt); 
                break;
            case Define.State.Moving:
                OnMouseEvent_IdleRun(evt);
                break;
            case Define.State.Skill:

                //if (evt == Define.MouseEvent.PointerUp)
                //_stopSkill = true; 
                OnMouseEvent_IdleRun(evt);
                break; 
        }
    }

    void OnMouseEvent_IdleRun(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        switch (evt)
        {
            case Define.MouseEvent.PointerDown:
                {
                    if (raycastHit)
                    {
                        _destPos = hit.point;
                        State = Define.State.Moving;
                        _stopSkill = false;

                        if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
                        {
                            _lockTarget = hit.collider.gameObject;
                        }
                        else
                        {
                            _lockTarget = null;
                            _stopSkill = true;
                        }
                    }
                }
                break;
            case Define.MouseEvent.Press:
                {
                    if (_lockTarget == null && raycastHit)
                        _destPos = hit.point;                    
                }
                break;
            case Define.MouseEvent.PointerUp:
                //                _stopSkill = true;
                if (_lockTarget == null)
                    _stopSkill = true;

                break;
        }
    }
}

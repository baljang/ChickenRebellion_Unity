using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class GameData
{
    public string Name;

    public bool[] keys = new bool[6];

    public int entireEnemy =0;
    public int remainEnemy =0;

    public int escapedChicken =0;
    public int remainChicken =1;

    public bool remainHero = true;

    public int stage = 0;

    public bool gmaeOver = false; 

}

public class GameManagerEx
{
    // int <-> GameObject

    GameData _gameData = new GameData();
    public GameData SaveData { get { return _gameData; } set { _gameData = value; } }

    public bool autoMode = false;

    public Define.Language lang = Define.Language.Jpanese; 

    #region 스탯
    public string Name
    {
        get { return _gameData.Name; }
        set { _gameData.Name = value; }
    }
    #endregion

    //GameObject _player;
    List<GameObject> _players = new List<GameObject>();
    //Dictionary<int, GameObject> _players = new Dictionary<int, GameObject>(); 
    HashSet<GameObject> _monsters = new HashSet<GameObject>();

    public Action<int> OnMonsterSpawnEvent = null;

    public Action<int> OnPlayerSpawnEvent = null;

    public List<GameObject> GetPlayers() { return _players; }

    public HashSet<GameObject> GetMonsters() { return _monsters; }

    public GameObject Spawn(Define.WorldObject type, string path, Transform parent = null) 
    {
        GameObject go = Managers.Resource.Instantiate(path, parent);

        switch(type)
        {
            case Define.WorldObject.Monster:
                _monsters.Add(go);
                if (OnMonsterSpawnEvent != null)
                    OnMonsterSpawnEvent.Invoke(1);
                Managers.UI.FindPopup<UI_HudPopup>().RefreshEnemyNum(); 
                Managers.Game.SaveData.entireEnemy += 1;
                break;
            case Define.WorldObject.Player:
                _players.Add(go);
                if (OnPlayerSpawnEvent != null)
                    OnPlayerSpawnEvent.Invoke(1);

                Managers.Game.SaveData.escapedChicken += 1;

                if (Managers.UI.FindPopup<UI_HudPopup>() == null)
                { }
                else
                {
                    Managers.UI.FindPopup<UI_HudPopup>().RefreshChickenNum();
                    Managers.UI.FindPopup<UI_HudPopup>().RefreshTotalChickenNum();
                }
                break;      
        }

        UnitController uc = go.GetComponent<UnitController>(); 
        if( uc  != null)
        Managers.Unit.AddUnit(uc);

        return go;

    }

    public Define.WorldObject GetWorldObjectType(GameObject go)
    {
        BaseController bc = go.GetComponent<BaseController>();
        if (bc == null)
            return Define.WorldObject.Unknown;

        return bc.WorldObjectType; 
    }

    public void Despawn(GameObject go)
    {
        Define.WorldObject type = GetWorldObjectType(go);

        switch(type)
        {
            case Define.WorldObject.Monster:
                {
                    if(_monsters.Contains(go))
                    {
                        _monsters.Remove(go);

                        if (OnMonsterSpawnEvent != null)
                            OnMonsterSpawnEvent.Invoke(-1);

                        Managers.UI.FindPopup<UI_HudPopup>().RefreshEnemyNum();
                    }
                }
                break;
            case Define.WorldObject.Player:
                {
                    if (_players.Contains(go))
                    {
                        _players.Remove(go);

                        if (OnPlayerSpawnEvent != null)
                            OnPlayerSpawnEvent.Invoke(-1);

                        Managers.UI.FindPopup<UI_HudPopup>().RefreshChickenNum();
                    }
                    else
                        return; 
                }
                break;  
        }

        Managers.Resource.Destroy(go); 
    }   

    public void MonsterAndPlayerClear()
    {
        _monsters.Clear();
        _players.Clear(); 
    }
}

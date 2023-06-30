
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성이 보장된다.
    static Managers Instance { get { Init(); return s_instance; } } // 유일한 매니져를 갖고온다

    #region Contents
    GameManagerEx _game = new GameManagerEx();
    public static GameManagerEx Game { get { return Instance._game; } }
    #endregion

    LockManager _lock = new LockManager(); 

    #region Core
    DataManager _data = new DataManager(); 
    InputManager _input = new InputManager();
    PoolManager _pool = new PoolManager(); 
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    RTSUnitManager _unitManager = new RTSUnitManager(); 

    public static DataManager Data { get { return Instance._data; } }
    public static InputManager Input { get { return Instance._input; } }
    public static PoolManager Pool { get { return Instance._pool; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static SceneManagerEx Scene { get { return Instance._scene; } }
    public static SoundManager Sound { get { return Instance._sound; } }
    public static UIManager UI { get { return Instance._ui; } }
    public static RTSUnitManager Unit {  get { return Instance._unitManager; } }

    public static LockManager Lock { get { return Instance._lock; } }
    #endregion

    public static string GetText(int id)
    {
        if (Managers.Game.lang == Define.Language.Jpanese)
        {
            if (Managers.Data.Texts.TryGetValue(id, out TextData value) == false)
                return "";
            if (id == 20048 || id == 20049 || id == 20050)
            {
                value.jpn = value.jpn.Replace("{totalChicken}", Managers.Game.SaveData.escapedChicken.ToString());
                value.jpn = value.jpn.Replace("{remainChicken}", Managers.Game.SaveData.remainChicken.ToString());
            }
            return value.jpn.Replace("{userName}", Managers.Game.Name);
        }
        else if (Managers.Game.lang == Define.Language.Korean)
        {
            if (Managers.Data.Texts.TryGetValue(id, out TextData value) == false)
                return "";
            if (id == 20048 || id == 20049 || id == 20050)
            {
                value.kor = value.kor.Replace("{totalChicken}", Managers.Game.SaveData.escapedChicken.ToString());
                value.kor = value.kor.Replace("{remainChicken}", Managers.Game.SaveData.remainChicken.ToString());
            }
            return value.kor.Replace("{userName}", Managers.Game.Name);
        }
        else return ""; 

    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        _input.OnUpdate();
    }

    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if (go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();

            s_instance._data.Init(); 
            s_instance._pool.Init(); 
            s_instance._sound.Init();
            s_instance._unitManager.Init();
         //   s_instance._lock.Init(); 
        }
    }

    public static void Clear()
    {
        Input.Clear(); 
        Sound.Clear();
        Scene.Clear(); 
        UI.Clear();
        Pool.Clear();
    //    Unit.Clear();
    //    Lock.Clear(); 
    }
}

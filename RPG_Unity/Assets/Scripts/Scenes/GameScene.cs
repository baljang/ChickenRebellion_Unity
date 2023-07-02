using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();

        Managers.Sound.Clear();
        Managers.Sound.Play("BGM1", Define.Sound.Bgm);

        Managers.Game.MonsterAndPlayerClear();
        Managers.Lock.ReleaseLocks();
        Managers.Game.SaveData.entireEnemy = 0;
        Managers.Game.SaveData.escapedChicken = 0;
        Managers.Game.SaveData.stage = 0;
        Managers.Game.SaveData.remainChicken = 0;

        if (Managers.Game.lang == Define.Language.Jpanese)
        {
            Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.ChickenSay, (int)UI_PlayPopup_JP.States.KeyInfo, () =>
            {
                Managers.UI.ShowPopupUI<UI_HudPopup>();
            });
        }
        else if (Managers.Game.lang == Define.Language.Korean)
        {
            Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.ChickenSay, (int)UI_PlayPopup_KR.States.KeyInfo, () =>
            {
                Managers.UI.ShowPopupUI<UI_HudPopup>();
            });
        }

        SceneType = Define.Scene.Game;
        Managers.UI.ShowSceneUI<UI_Inven>();
        Dictionary<int, Data.Stat> dict = Managers.Data.StatDict;
        gameObject.GetOrAddComponent<CursorController>();

        Managers.Unit.Release();
        Managers.Unit.Init();

        GameObject player = Managers.Game.Spawn(Define.WorldObject.Player, "ChickenPlayer");
        player.GetComponent<PlayerStat>().SetStat(2); 
        Camera.main.gameObject.GetOrAddComponent<CameraController>().SetPlayer(player);

        //Managers.Game.Spawn(Define.WorldObject.Monster, "Chicken");
        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();

        Managers.Unit.mouseDrag.dragRectangle = GameObject.Find("Canvas/DragRectangle").GetComponent<RectTransform>();
        Managers.Unit.mouseDrag.Init();

        Managers.Lock.ReleaseLocks();
        Managers.Lock.Init();

        Managers.Game.SaveData.gmaeOver = false;
        Managers.Game.SaveData.clear = false; 
    }

    public override void Clear()
    {
        
    }

    private void Update()
    {
        if (Input.GetKey("escape"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Debug.Log("게임 종료");
        Application.Quit();
#endif
        }
    }
}

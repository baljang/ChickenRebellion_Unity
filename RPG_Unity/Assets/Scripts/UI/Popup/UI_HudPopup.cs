using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HudPopup : UI_Popup
{
    public enum Texts
    {
        RemainEnemyText,
        RemainChickenText,
        EscapedChickenText,
        SelectAllText,
        AutoBattleText
    }

    public enum Buttons
    {
        SellectAll,
        AutoBattle
    }

    GameManagerEx _game;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        _game = Managers.Game;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons));

        GetButton((int)Buttons.SellectAll).gameObject.BindEvent(OnClickSelectAllButton);
        GetButton((int)Buttons.AutoBattle).gameObject.BindEvent(OnClickAutoBattleButton);

        RefreshUI(); 

        return true; 
    }

    void OnClickSelectAllButton()
    {
        Debug.Log("OnClickSelectAllButton");
        Managers.Unit.SelectAll();
    }

    void OnClickAutoBattleButton()
    {
        Debug.Log("OnClickAutoBattleButton");
        if (Managers.Game.autoMode == false)
        {
            GetButton((int)Buttons.AutoBattle).image.sprite = Managers.Resource.Load<Sprite>("Sprites/btn_15");

            Managers.Game.autoMode = true;
        }
        else
        {
            GetButton((int)Buttons.AutoBattle).image.sprite = Managers.Resource.Load<Sprite>("Sprites/btn_07");

            Managers.Game.autoMode = false;
        }
    }

    public void RefreshUI()
    {
        if (_init == false)
            return;

        RefreshStat(); 
    }

    public void RefreshStat()
    {
        RefreshEnemyNum();
        RefreshChickenNum();
        RefreshTotalChickenNum(); 
    }

    public void RefreshEnemyNum()
    {
        int enemyNum = Managers.Game.SaveData.remainEnemy;
        GetText((int)Texts.RemainEnemyText).text = $"Enemy: {(int)enemyNum}";
    }

    public void RefreshChickenNum()
    {
        int chickenNum = Managers.Game.SaveData.remainChicken;
        GetText((int)Texts.RemainChickenText).text = $"Chicken: {(int)chickenNum}"; 
    }

    public void RefreshTotalChickenNum()
    {
        int tatolChickenNum = Managers.Game.SaveData.escapedChicken;
        GetText((int)Texts.EscapedChickenText).text = $"Total: {(int)tatolChickenNum}"; 
    }

    private void Update()
    {
        if (Managers.Game.SaveData.stage == 1 && Managers.Game.SaveData.remainEnemy == 0)
        {

            if (Managers.UI.PopupStackCheck(1) && Managers.Game.SaveData.keys[1] == false)
            {
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.DogsEnd, (int)UI_PlayPopup_JP.States.NextInfo, () =>
                    {
                        Managers.Game.SaveData.keys[1] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.DogsEnd, (int)UI_PlayPopup_KR.States.NextInfo, () =>
                    {
                        Managers.Game.SaveData.keys[1] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
            }
        }

        if(Managers.Game.SaveData.stage == 2 && Managers.Game.SaveData.remainEnemy == 0)
        {
            if (Managers.UI.PopupStackCheck(1) && Managers.Game.SaveData.keys[2] == false)
            {
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.FreshEnd, (int)UI_PlayPopup_JP.States.ChickenGoNext, () =>
                    {
                        Managers.Game.SaveData.keys[2] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.FreshEnd, (int)UI_PlayPopup_KR.States.ChickenGoNext, () =>
                    {
                        Managers.Game.SaveData.keys[2] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
            }
        }

        if (Managers.Game.SaveData.stage == 3 && Managers.Game.SaveData.remainEnemy == 0)
        {
            if (Managers.UI.PopupStackCheck(1) && Managers.Game.SaveData.keys[3] == false)
            {
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.ElderEnd, (int)UI_PlayPopup_JP.States.ChickenGogo, () =>
                    {
                        Managers.Game.SaveData.keys[3] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.ElderEnd, (int)UI_PlayPopup_KR.States.ChickenGogo, () =>
                    {
                        Managers.Game.SaveData.keys[3] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
            }               
        }

        if (Managers.Game.SaveData.stage == 4 && Managers.Game.SaveData.remainEnemy == 0)
        {
            if (Managers.UI.PopupStackCheck(1) && Managers.Game.SaveData.keys[4] == false)
            {
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.PoliceEnd, (int)UI_PlayPopup_JP.States.ChickenGoEnd, () =>
                    {
                        Managers.Game.SaveData.keys[4] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.PoliceEnd, (int)UI_PlayPopup_KR.States.ChickenGoEnd, () =>
                    {
                        Managers.Game.SaveData.keys[4] = true;
                        Managers.UI.SceneUIUpdate();
                    });
                }
            }
        }

        if (Managers.Game.SaveData.stage == 5 && Managers.Game.SaveData.remainEnemy == 0)
        {
            if (Managers.UI.PopupStackCheck(1) && Managers.Game.SaveData.keys[5] == false)
            {
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.BearEnd, (int)UI_PlayPopup_JP.States.Ending, () =>
                    {
                        Managers.Game.MonsterAndPlayerClear();
                        Managers.Game.SaveData.keys[5] = true;
                        //    Managers.UI.SceneUIUpdate();
                        Managers.Lock.ReleaseLocks();
                        Managers.Game.SaveData.entireEnemy = 0;
                        Managers.Game.SaveData.escapedChicken = 0;
                        Managers.Game.SaveData.stage = 0;
                        Managers.Game.SaveData.remainChicken = 0;

                        Managers.Game.SaveData.keys[5] = false;
                        // 타이틀 화면으로
                        Managers.Scene.LoadScene(Define.Scene.Title);
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.BearEnd, (int)UI_PlayPopup_KR.States.Ending, () =>
                    {
                        Managers.Game.MonsterAndPlayerClear();
                        Managers.Game.SaveData.keys[5] = true;
                        //    Managers.UI.SceneUIUpdate();
                        Managers.Lock.ReleaseLocks();
                        Managers.Game.SaveData.entireEnemy = 0;
                        Managers.Game.SaveData.escapedChicken = 0;
                        Managers.Game.SaveData.stage = 0;
                        Managers.Game.SaveData.remainChicken = 0;

                        Managers.Game.SaveData.keys[5] = false;
                        // 타이틀 화면으로
                        Managers.Scene.LoadScene(Define.Scene.Title);
                    });
                }
            }
        }

        if(Managers.Game.SaveData.stage != 0 && Managers.Game.SaveData.remainChicken == 0 && Managers.Game.SaveData.gmaeOver == false)
        {
            Managers.Game.SaveData.gmaeOver = true;
            if (Managers.Game.lang == Define.Language.Jpanese)
            {
                Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.BadEnding, (int)UI_PlayPopup_JP.States.BadEnding, () =>
                {
                    Managers.Game.MonsterAndPlayerClear();
                    Managers.Game.SaveData.entireEnemy = 0;
                    Managers.Game.SaveData.remainEnemy = 0;
                    Managers.Game.SaveData.escapedChicken = 0;
                    Managers.Game.SaveData.stage = 0;

                    Managers.Lock.ReleaseLocks();

                    // 타이틀 화면으로
                    Managers.Scene.LoadScene(Define.Scene.Title);
                });
            }
            else if (Managers.Game.lang == Define.Language.Korean)
            {
                Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.BadEnding, (int)UI_PlayPopup_KR.States.BadEnding, () =>
                {
                    Managers.Game.MonsterAndPlayerClear();
                    Managers.Game.SaveData.entireEnemy = 0;
                    Managers.Game.SaveData.remainEnemy = 0;
                    Managers.Game.SaveData.escapedChicken = 0;
                    Managers.Game.SaveData.stage = 0;

                    Managers.Lock.ReleaseLocks();

                    // 타이틀 화면으로
                    Managers.Scene.LoadScene(Define.Scene.Title);
                });
            }
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    [SerializeField]
    public int _keyNum;

    [SerializeField]
    int _chickenNum;

    [SerializeField]
    public int _enemyNum;

    [SerializeField]
    public string _kindEnemy;

    [SerializeField]
    GameObject _fire;

    [SerializeField]
    public GameObject _lockVisual;

    [SerializeField]
    public GameObject _info; 

    public int _level; 

    private bool _playerApproached;

    private bool _isUnlocked = false; 

    BoxCollider _collision;
    Action _interact;

    bool _playerKeyMatch;

    // Start is called before the first frame update
    void Start()
    {
        Init();  
    }

    private void Init()
    {
        _collision = GetComponent<BoxCollider>();

        // test
        Managers.Game.SaveData.keys[0] = true;
        Managers.UI.SceneUIUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerApproached && _isUnlocked == false)
            _info.SetActive(true);
        else
            _info.SetActive(false); 

        if(_isUnlocked == false && Input.GetKeyDown(KeyCode.F) && _playerApproached && Managers.Game.SaveData.keys[_keyNum] == true)
        {
            Unlock(); 
        }

        if (_isUnlocked == false && GetComponent<Stat>().Hp == 0)
            Unlock();   
    }

    private void Unlock()
    {
        _isUnlocked = true; 
        Managers.Game.SaveData.stage++;

        GetComponent<Stat>().Hp = 0;

        GameObject go = new GameObject { name = "SpawningPool" };
        SpawningPool pool = go.GetOrAddComponent<SpawningPool>();
        pool.Spawn(this.gameObject, _chickenNum, Define.WorldObject.Player, "ChickenPlayer", 1);

        //for (int i=0; i<5; i++)
        //    Managers.Game.Spawn(Define.WorldObject.Player, "ChickenPlayer");

        pool.Spawn(this.gameObject, _enemyNum, Define.WorldObject.Monster, _kindEnemy, _level);
        switch (_keyNum)
        {
            case 0:
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.DogStage, (int)UI_PlayPopup_JP.States.ChikenEscape, () =>
                    {
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.DogStage, (int)UI_PlayPopup_KR.States.ChikenEscape, () =>
                    {
                    });
                }
                break;

            case 1:
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.FreshStage, (int)UI_PlayPopup_JP.States.ChickenDeclare, () =>
                    {
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.FreshStage, (int)UI_PlayPopup_KR.States.ChickenDeclare, () =>
                    {
                    });
                }
                break;

            case 2:
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.ElderStage, (int)UI_PlayPopup_JP.States.ChickenNoSatisfy, () =>
                    {
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.ElderStage, (int)UI_PlayPopup_KR.States.ChickenNoSatisfy, () =>
                    {
                    });
                }
                break;

            case 3:
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.PoliceStage, (int)UI_PlayPopup_JP.States.ChickenViolence, () =>
                    {
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.PoliceStage, (int)UI_PlayPopup_KR.States.ChickenViolence, () =>
                    {
                    });
                }
                break;

            case 4:
                if (Managers.Game.lang == Define.Language.Jpanese)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_JP>().SetInfo((int)UI_PlayPopup_JP.States.BearStage, (int)UI_PlayPopup_JP.States.ReplyToBear, () =>
                    {
                    });
                }
                else if (Managers.Game.lang == Define.Language.Korean)
                {
                    Managers.UI.ShowPopupUI<UI_PlayPopup_KR>().SetInfo((int)UI_PlayPopup_KR.States.BearStage, (int)UI_PlayPopup_KR.States.ReplyToBear, () =>
                    {
                    });
                }
                break;
        }
        _fire.SetActive(true);
        _lockVisual.SetActive(false);
        SetLayerRecursively(gameObject, LayerMask.NameToLayer("Default"));
    }

    void SetLayerRecursively(GameObject obj, int newLayer)
    {
        if (null == obj)
        {
            return;
        }

        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            if (null == child)
            {
                continue;
            }
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController _playerController = other.GetComponent<PlayerController>();
        _playerApproached = _playerController != null;
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController _playerController = other.GetComponent<PlayerController>();
        if (_playerController != null)
            _playerApproached = false;
    }
}

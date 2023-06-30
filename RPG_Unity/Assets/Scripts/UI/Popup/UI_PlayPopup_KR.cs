using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PlayPopup_KR : UI_Popup
{
    public enum States
    {
        ChickenSay,
        StartInfo, 
        KeyInfo, 
        DogStage,  
        replyToDog,
        ChikenEscape, 
        DogsEnd,
        GetKey,
        NextInfo,

        FreshStage,
        ChickenDeclare,

        FreshEnd,
        ChickenGoNext,

        ElderStage, 
        ChickenNoSatisfy,

        ElderEnd,
        ChickenGogo,

        PoliceStage, 
        ChickenViolence,

        PoliceEnd, 
        ChickenGoEnd,

        BearStage,
        ReplyToBear,

        BearEnd, 
        ChickenEnd,

        Result,
        Ending,

        BadEnding
    }

    public enum Texts
    {
        PlayText
    }

    Action _onEndCallback; 
    public int _selectedIndex;
    int _startIndex = (int)States.StartInfo;
    int _lastIndex = (int)States.Ending;

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(States));
        BindText(typeof(Texts)); 


        gameObject.BindEvent(OnClickImage); 

        _selectedIndex = _startIndex; 

        RefreshUI(); 

        return true; 
    }

    public void SetInfo(int startIndex, int endIndex, Action onEndCallback)
    {
        // 게임을 일시정지합니다.
        Time.timeScale = 0;

        _onEndCallback = onEndCallback;
        _selectedIndex = startIndex;
        _startIndex = startIndex;
        _lastIndex = endIndex;
        RefreshUI();
    }


    void RefreshUI()
    {
        if (_init == false)
            return;

        GetObject((int)States.ChickenSay).SetActive(false);
        GetObject((int)States.StartInfo).SetActive(false);
        GetObject((int)States.KeyInfo).SetActive(false);
        GetObject((int)States.DogStage).SetActive(false);
        GetObject((int)States.replyToDog).SetActive(false);
        GetObject((int)States.ChikenEscape).SetActive(false);
        GetObject((int)States.DogsEnd).SetActive(false);
        GetObject((int)States.GetKey).SetActive(false);
        GetObject((int)States.NextInfo).SetActive(false);

        GetObject((int)States.FreshStage).SetActive(false);
        GetObject((int)States.ChickenDeclare).SetActive(false);

        GetObject((int)States.FreshEnd).SetActive(false);
        GetObject((int)States.ChickenGoNext).SetActive(false);

        GetObject((int)States.ElderStage).SetActive(false);
        GetObject((int)States.ChickenNoSatisfy).SetActive(false);

        GetObject((int)States.ElderEnd).SetActive(false);
        GetObject((int)States.ChickenGogo).SetActive(false);

        GetObject((int)States.PoliceStage).SetActive(false);
        GetObject((int)States.ChickenViolence).SetActive(false);

        GetObject((int)States.PoliceEnd).SetActive(false);
        GetObject((int)States.ChickenGoEnd).SetActive(false);

        GetObject((int)States.BearStage).SetActive(false);
        GetObject((int)States.ReplyToBear).SetActive(false);

        GetObject((int)States.BearEnd).SetActive(false);
        GetObject((int)States.ChickenEnd).SetActive(false);

        GetObject((int)States.Result).SetActive(false);
        GetObject((int)States.Ending).SetActive(false);

        GetObject((int)States.BadEnding).SetActive(false);


        if (_selectedIndex <= (int)States.BadEnding)
            GetObject(_selectedIndex).SetActive(true);

        switch (_selectedIndex)
        {
            case (int)States.ChickenSay:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play1);
                break;
            case (int)States.StartInfo:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play2);
                break;
            case (int)States.KeyInfo:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play3);
                break;
            case (int)States.DogStage:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play4);
                break;
            case (int)States.replyToDog:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play5);
                break;
            case (int)States.ChikenEscape:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play6);
                break;
            case (int)States.DogsEnd:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play7);
                break;
            case (int)States.GetKey:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play8);
                break;
            case (int)States.NextInfo:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play9);
                break;

            case (int)States.FreshStage:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play10);
                break;
            case (int)States.ChickenDeclare:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play11);
                break;

            case (int)States.FreshEnd:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play12);
                break;
            case (int)States.ChickenGoNext:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play13);
                break;

            case (int)States.ElderStage:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play14);
                break;
            case (int)States.ChickenNoSatisfy:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play15);
                break;

            case (int)States.ElderEnd:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play16);
                break;
            case (int)States.ChickenGogo:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play17);
                break;

            case (int)States.PoliceStage:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play18);
                break;
            case (int)States.ChickenViolence:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play19);
                break;

            case (int)States.PoliceEnd:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play20);
                break;
            case (int)States.ChickenGoEnd:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play21);
                break;

            case (int)States.BearStage:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play22);
                break;
            case (int)States.ReplyToBear:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play23);
                break;

            case (int)States.BearEnd:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play24);
                break;
            case (int)States.ChickenEnd:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play25);
                break;

            case (int)States.Result:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play26);
                break;
            case (int)States.Ending:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play27);
                break;

            case (int)States.BadEnding:
                GetText((int)Texts.PlayText).text = Managers.GetText(Define.Play28);
                break;

            default:
                GetText((int)Texts.PlayText).text = "";
                break;
        }
    }

    void OnClickImage()
    {
        Debug.Log("OnClickImage");

        // 끝났으면 닫는다
        if (_selectedIndex == _lastIndex)
        {
            Managers.UI.ClosePopupUI(this);
            _onEndCallback?.Invoke();

            // 게임을 재개합니다.
            Time.timeScale = 1;
            return;
        }

        // 다음 것으로 이동
        _selectedIndex++;
        RefreshUI();
    }
}

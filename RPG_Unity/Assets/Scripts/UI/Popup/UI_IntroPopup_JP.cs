using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_IntroPopup_JP : UI_Popup
{
    public enum GameObjects
    {
        Intro1,
        Intro2,
        Intro3,
        Intro4
    }

    enum Texts
    {
        IntroText
    }

    Action _onEndCallback;
    int _selectedIndex;
    int _startIndex = (int)GameObjects.Intro1;
    int _lastIndex = (int)GameObjects.Intro4; 

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        gameObject.BindEvent(OnClickImage);

        _selectedIndex = _startIndex;

        RefreshUI();

        return true; 
    }

    public void SetInfo(int startIndex, int endIndex, Action onEndCallback)
    {
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

        GetObject((int)GameObjects.Intro1).SetActive(false);
        GetObject((int)GameObjects.Intro2).SetActive(false);
        GetObject((int)GameObjects.Intro3).SetActive(false);
        GetObject((int)GameObjects.Intro4).SetActive(false);

        if (_selectedIndex <= (int)GameObjects.Intro4)
            GetObject(_selectedIndex).SetActive(true);

        switch (_selectedIndex)
        {
            case (int)GameObjects.Intro1:
                GetText((int)Texts.IntroText).text = Managers.GetText(Define.Intro1);
                break;
            case (int)GameObjects.Intro2:
                GetText((int)Texts.IntroText).text = Managers.GetText(Define.Intro2);
                break;
            case (int)GameObjects.Intro3:
                GetText((int)Texts.IntroText).text = Managers.GetText(Define.Intro3);
                break;
            case (int)GameObjects.Intro4:
                GetText((int)Texts.IntroText).text = Managers.GetText(Define.Intro4);
                break;
            default:
                GetText((int)Texts.IntroText).text = "";
                break;
        }
    }

    void OnClickImage()
    {
        Debug.Log("OnClickImage");

        // 끝났으면 닫는다
        if (_selectedIndex == (int)GameObjects.Intro4)
        {
            Managers.UI.ClosePopupUI(this);
            _onEndCallback?.Invoke();
            return;
        }

        // 다음 것으로 이동
        _selectedIndex++;

        if (_selectedIndex > (int)GameObjects.Intro4)
            return; 
        RefreshUI();
    }
}

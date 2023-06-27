using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
//    int _maskUnit = 1 << (int)Define.Layer.Unit; 

    public Action KeyAction = null;
    public Action<Define.MouseEvent> MouseAction = null;
    public Action<Define.MouseEvent> UnitMouseAction = null; 

    bool _LeftPressed= false;
    bool _rightPressed = false; 
    float _pressedTime = 0; 
    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return; 

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke(); 

        if(MouseAction!= null)
        {

            // 마우스 왼쪽 클릭으로 유닛 선택 or 해제
            if (Input.GetMouseButton(0))
            {
                if (!_LeftPressed)
                {
                    UnitMouseAction.Invoke(Define.MouseEvent.LeftPointerDown);
                    _pressedTime = Time.time;
                }
                UnitMouseAction.Invoke(Define.MouseEvent.Drag);
                _LeftPressed = true;

            }
            else
            {
                if (_LeftPressed)
                {

                    //if (Time.time < _pressedTime + 0.2f)
                    //    UnitMouseAction.Invoke(Define.MouseEvent.Click);
                    UnitMouseAction.Invoke(Define.MouseEvent.PointerUp);

                }
                _LeftPressed = false;
                _pressedTime = 0;
            }

            if (Input.GetMouseButton(1))
            {
                if(!_rightPressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.PointerDown);
                    _pressedTime = Time.time; 
                }
                MouseAction.Invoke(Define.MouseEvent.Press);
                _rightPressed = true; 
            }
            else
            {
                if (_rightPressed)
                {
                    if(Time.time < _pressedTime + 0.2f)
                        MouseAction.Invoke(Define.MouseEvent.Click);
                    MouseAction.Invoke(Define.MouseEvent.PointerUp);
                }
                _rightPressed = false;
                _pressedTime = 0; 
            }
        }
    }

    public void Clear()
    {
        KeyAction = null;
        MouseAction = null; 
    }
}

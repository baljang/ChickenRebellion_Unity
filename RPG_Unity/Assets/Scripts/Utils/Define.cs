using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Define 
{
    public enum WorldObject
    {
        Unknown, 
        Player, 
        Monster,
    }

    public enum State
    {
        Die,
        Moving,
        Idle,
        Skill,
    }

public enum Layer
    {
        UI = 5, 
        Monster = 8, 
        Ground = 9, 
        Block = 10, 
        Unit= 11,        
    }

    public enum Scene
    {
        Unknown, 
        Login, 
        Title,
        Lobby, 
        Game, 
    }

    public enum Sound
    {
        Bgm, 
        Effect, 
        MaxCount, 
    }

    public enum UIEvent
    {
        Click,
        Drag,
        Pressed,
        PointerDown,
        PointerUp,
    }

    public enum MouseEvent
    {
        LeftPointerDown,
        Press, 
        PointerDown, 
        PointerUp,
        Click, 
        Drag,
    }

    public enum CameraMode
    {
        QuarterView, 
    }

    public enum Language
    {
        Jpanese, 
        Korean, 
        English
    }

    public const int Intro1 = 20019;
    public const int Intro2 = 20020;
    public const int Intro3 = 20021;
    public const int Intro4 = 20022;

    public const int Play1 = 20023;
    public const int Play2 = 20024;
    public const int Play3 = 20025;
    public const int Play4 = 20026;
    public const int Play5 = 20027;
    public const int Play6 = 20028;

    public const int Play7 = 20029;
    public const int Play8 = 20030;

    public const int Play9 = 20031;
    public const int Play10 = 20032;

    public const int Play11 = 20033;
    public const int Play12 = 20034;

    public const int Play13 = 20035;
    public const int Play14 = 20036;

    public const int Play15 = 20037;
    public const int Play16 = 20038;

    public const int Play17 = 20039;
    public const int Play18 = 20040;

    public const int Play19 = 20041;
    public const int Play20 = 20042;

    public const int Play21 = 20043;
    public const int Play22 = 20044;

    public const int Play23 = 20045;
    public const int Play24 = 20046;

    public const int Play25 = 20047;
    public const int Play26 = 20048;

    public const int Play27 = 20049;

    public const int Play28 = 20050;


}

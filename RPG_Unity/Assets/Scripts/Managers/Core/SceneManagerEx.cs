using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public void LoadScene(Define.Scene type)
    {
        if(type == Define.Scene.Game)
        {
            for (int i = 1; i < 6; i++)
                Managers.Game.SaveData.keys[i] = false;

        }

        Managers.Clear(); 
        SceneManager.LoadScene(GetSceneName(type)); 
    }

    string GetSceneName(Define.Scene type)
    {
        string name = System.Enum.GetName(typeof(Define.Scene), type);
        return name; 
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}

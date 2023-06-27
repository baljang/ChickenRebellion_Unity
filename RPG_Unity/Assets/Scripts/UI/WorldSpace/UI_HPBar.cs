using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_HPBar : UI_Base
{
    enum GameObjects
    {
        HPBar, 
        LevelText
    }

    Stat _stat; 

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();

        return true; 
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = parent.position + Vector3.up * (parent.GetComponent<Collider>().bounds.size.y);
        transform.rotation = Camera.main.transform.rotation;

        float ratio = _stat.Hp / (float)_stat.MaxHp; 
        SetHPRatio(ratio);

        SetHPRatio(_stat.Level); 
    }

    public void SetHPRatio(float ratio)
    {
        GetObject((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;      
    }

    public void SetHPRatio(int level)
    {
        GetObject((int)GameObjects.LevelText).GetComponent<TextMeshProUGUI>().text = "Level: " + level.ToString(); 
    }
}

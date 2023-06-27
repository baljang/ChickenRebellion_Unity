using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inven_Item : UI_Base
{
    enum GameObjects
    {
        ItemIcon,
        ItemNameText, 
    }

    string _name; 

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<GameObject>(typeof(GameObjects));
 //       Get<GameObject>((int)GameObjects.ItemNameText).GetComponent<TextMeshProUGUI>().text = _name;

 //       Get<GameObject>((int)GameObjects.ItemIcon).BindEvent(() => { Debug.Log($"아이템 클릭! {_name}");  });

        return true; 
    }

 //   public void SetImage()
 //   {
 //       string imagePath = "Sprites/" + _name; // 경로 수정 필요
 //       Sprite itemSprite = Resources.Load<Sprite>(imagePath);

 //       if (itemSprite != null)
 //       {
 //           GameObject itemIconObject = Get<GameObject>((int)GameObjects.ItemIcon);
 //           if (itemIconObject == null)
 //           {
 //               Debug.LogError($"Failed to get ItemIcon GameObject");
 //               return;
 //           }
 //           Image iconImage = itemIconObject.GetComponent<Image>();

 ////           Image iconImage = Get<GameObject>((int)GameObjects.ItemIcon).GetComponent<Image>();
 //           iconImage.sprite = itemSprite;
 //       }
 //       else
 //       {
 //           Debug.LogError($"Failed to load sprite from {imagePath}");
 //       }
    //}

    public void SetInfo(string name)
    {
        _name = name;
        //SetImage(); 
    }
}

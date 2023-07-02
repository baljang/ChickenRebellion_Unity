using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven : UI_Scene
{
    enum GameObjects
    {
         GridPanel
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        Bind<GameObject>(typeof(GameObjects));

        UpdateInventory();

        return true; 
    }

    public void UpdateInventory()
    {
        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);

        if (gridPanel == null)
            return; 

        // Remove all current items in the inventory
        foreach (Transform child in gridPanel.transform)
            Managers.Resource.Destroy(child.gameObject);

        // Populate the inventory based on the current save data
        for (int i = 0; i < 5; i++)
        {
            if (Managers.Game.SaveData.keys[i])
            {
                GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(parent: gridPanel.transform).gameObject;
                UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
                invenItem.SetInfo($"Key{i + 1}");
            }
        }
    }
}

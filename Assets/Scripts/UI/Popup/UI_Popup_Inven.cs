using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Inven : UI_Popup
{
    enum GameObjects
    {
       
    }

    int maxInvenLength = 10;


    private void OnEnable()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;


        BindObject(typeof(GameObjects));

 
 
        for (int i = 0; i < maxInvenLength; i++)
        {

      //      GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");


            //    GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.transform).gameObject;            
            ////     UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>();
            //    invenItem.SetInfo($"집행검{i}번");
        }

        return true;
    }
}

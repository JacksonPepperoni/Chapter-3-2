using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Slot : UI_Base
{
    enum Buttons
    {

    }

    enum Texts
    {
    }

    enum GameObjects
    {
        EquipCheck
    }

    enum Images
    {
        Highlight_Front,
        Highlight_Back,
        ItemIcon,
    }

    [HideInInspector] public Item_Equip item_Equip;

    void OnEnable()
    {
        Initialize();
   
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonEnter, Define.UIEvent.PointerEnter);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonExit, Define.UIEvent.PointerExit);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(OnButtonClick);

        GetObject((int)GameObjects.EquipCheck).SetActive(false);
        if (item_Equip != null)
            GetObject((int)GameObjects.EquipCheck).SetActive(item_Equip.isEquip);

        GetImage((int)Images.Highlight_Back).gameObject.SetActive(false);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(false);



        return true;
    }


    void OnButtonClick()
    {
        if (item_Equip == null) return;

        if (item_Equip.isEquip)
        {
            if (Managers.Game.player.UnEquip(item_Equip.id))
            {   item_Equip.isEquip = false;
                GetObject((int)GameObjects.EquipCheck).SetActive(false);

            }
        }
        else
        {
            if (Managers.Game.player.Equip(item_Equip.id))
            {
                item_Equip.isEquip = true; 
                GetObject((int)GameObjects.EquipCheck).SetActive(true); 
            }
               
        }
    }

    public void OnButtonEnter(BaseEventData data)
    {

        GetImage((int)Images.Highlight_Back).gameObject.SetActive(true);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(true);

        if (item_Equip != null) Managers.Game.OnEquipInfoEnter(item_Equip.id);

    }
    public void OnButtonExit(BaseEventData data)
    {
        GetImage((int)Images.Highlight_Back).gameObject.SetActive(false);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(false);

        if (item_Equip != null) Managers.Game.OnEquipInfoExit();
    }


}

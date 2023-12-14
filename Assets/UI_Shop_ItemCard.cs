using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Shop_ItemCard : UI_Base
{

    enum GameObjects
    {
        InfoPanel,
        EquipInvenPanel
    }

    enum Buttons
    {
        LeftBtn,
        RightBtn,
        CancelBtn
    }

    enum Texts
    {
        ItemNameText,
        ItemComentText,

    }
    enum Images
    {
        ItemIcon
    }

    [HideInInspector] public int itemId;
    void Start()
    {
        Initialize();
    }


    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));

        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonEnter, Define.UIEvent.PointerEnter);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonExit, Define.UIEvent.PointerExit);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(OnButtonClick);



        Refresh();


        return true;
    }

    void Refresh()
    {

        GetImage((int)Images.ItemIcon).sprite = Managers.Data.items_Equip[itemId].IconImage;
        GetImage((int)Images.ItemIcon).color = (Managers.Data.userData.invenGetArray[itemId]) ? Color.white : Color.gray;
        GetObject((int)GameObjects.InfoPanel).SetActive(Managers.Data.userData.isWearArray[itemId]);

    }



    void OnButtonClick()
    {
       
    }



    public void OnButtonEnter(BaseEventData data)
    {

    }
    public void OnButtonExit(BaseEventData data)
    {

        Managers.Game.OnEquipInfoExit?.Invoke();
    }

}

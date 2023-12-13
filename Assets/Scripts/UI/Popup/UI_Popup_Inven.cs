using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.Port;

public class UI_Popup_Inven : UI_Popup
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
        BuffText,
        ItemComentText,



    }
    enum Images
    {
        ItemIcon
    }


    private void OnEnable()
    {
        Initialize();

    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Bind<TextMeshProUGUI>(typeof(Texts));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));


        GetButton((int)Buttons.RightBtn).gameObject.BindEvent(OnBtnRight);
        GetButton((int)Buttons.LeftBtn).gameObject.BindEvent(OnBtnLeft);
        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnPointerDown);

        GetObject((int)GameObjects.InfoPanel).SetActive(false);

        AddSlot();


        Managers.Game.OnEquipInfoEnter -= ShowItemInfo;
        Managers.Game.OnEquipInfoEnter += ShowItemInfo;

        Managers.Game.OnEquipInfoExit -= CloseItemInfo;
        Managers.Game.OnEquipInfoExit += CloseItemInfo;

        Managers.Game.OnEquipChanged();



        return true;
    }


    private void AddSlot()
    {
        for (int i = 0; i < Managers.Game.player.inven.Length; i++)
        {
            UI_Inven_Slot item = Managers.Resource.InstantiatePrefab("UI_Inven_Slot.prefab", GetObject((int)GameObjects.EquipInvenPanel).transform).GetComponent<UI_Inven_Slot>();

            if (Managers.Game.player.inven[i] != null)
            {
                item.item_Equip = Managers.Game.player.inven[i];
                item.item_Equip.isEquip = Managers.Game.player.inven[i].isEquip;
            }

        }

    }

    public void ShowItemInfo(int id)
    {
        if (GetObject((int)GameObjects.InfoPanel) == null) return;

        GetObject((int)GameObjects.InfoPanel).SetActive(true);

        GetText((int)Texts.ItemNameText).text = Managers.Data.items_Equip[id].name;
        GetText((int)Texts.BuffText).text = $"공격력 : {Managers.Data.items_Equip[id].buff_Atk} / 방어력 : {Managers.Data.items_Equip[3].buff_Def} / 최대생명력 :  {Managers.Data.items_Equip[3].buff_MaxHp}";
        GetText((int)Texts.ItemComentText).text = Managers.Data.items_Equip[id].comment;
    }
    public void CloseItemInfo()
    {
        if (GetObject((int)GameObjects.InfoPanel) == null) return;

        GetObject((int)GameObjects.InfoPanel).SetActive(false);
    }


    public void OnBtnLeft()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup_State>();

    }
    public void OnBtnRight()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup_State>();

    }

    public void OnPointerDown()
    {
        Managers.UI.ClosePopupUI(this);

    }

}

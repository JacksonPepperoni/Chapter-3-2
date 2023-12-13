using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        Equip1,
        Equip2,
        Equip3,

        ItemIcon
    }


    public void OnEnabled()
    {
        Initialize();

    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindImage(typeof(Images));

        GetButton((int)Buttons.RightBtn).gameObject.BindEvent(OnBtnRight);
        GetButton((int)Buttons.LeftBtn).gameObject.BindEvent(OnBtnLeft);
        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnBtnCancel);

        GetObject((int)GameObjects.InfoPanel).SetActive(false);


        GetImage((int)Images.Equip1).gameObject.SetActive(false);
        GetImage((int)Images.Equip2).gameObject.SetActive(false);
        GetImage((int)Images.Equip3).gameObject.SetActive(false);

        Managers.Game.OnEquipInfoEnter -= ShowItemInfo;
        Managers.Game.OnEquipInfoEnter += ShowItemInfo;

        Managers.Game.OnEquipInfoExit -= CloseItemInfo;
        Managers.Game.OnEquipInfoExit += CloseItemInfo;

        Managers.Game.OnEquipChanged?.Invoke();

        AddSlot();

        return true;
    }



    private void AddSlot()
    {
        for (int i = 0; i < Managers.Data.items_Equip.Keys.Count; i++)
        {
            UI_Inven_Slot item = Managers.Resource.InstantiatePrefab("UI_Inven_Slot.prefab", GetObject((int)GameObjects.EquipInvenPanel).transform).GetComponent<UI_Inven_Slot>();
            item.slotNumber = i;
            item.item_Equip = Managers.Data.items_Equip[i];
        }

    }

    public void ShowItemInfo(int id)
    {


        GetText((int)Texts.ItemNameText).text = Managers.Data.items_Equip[id].name;
        GetText((int)Texts.BuffText).text =
                 ((Managers.Data.items_Equip[id].buff_Atk != 0) ? $"공격력 : {Managers.Data.items_Equip[id].buff_Atk}    " : "") +
                 ((Managers.Data.items_Equip[id].buff_Def != 0) ? $"방어력 : {Managers.Data.items_Equip[id].buff_Def}    " : "") +
                 ((Managers.Data.items_Equip[id].buff_MaxHp != 0) ? $"최대생명력 : {Managers.Data.items_Equip[id].buff_MaxHp}" : "");

        //  if(GetImage((int)Images.ItemIcon).sprite != null)
        GetImage((int)Images.ItemIcon).sprite = Managers.Data.items_Equip[id].IconImage;




        //  GetImage((int)Images.ItemIcon).sprite = Resources.Load<Sprite>($"Test/{Managers.Data.items_Equip[id].IconImage.name}");



        GetText((int)Texts.ItemComentText).text = Managers.Data.items_Equip[id].comment;

        if (GetObject((int)GameObjects.InfoPanel) != null)
            GetObject((int)GameObjects.InfoPanel).SetActive(true);

    }
    public void CloseItemInfo()
    {
        if (GetObject((int)GameObjects.InfoPanel) != null)
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

    public void OnBtnCancel()
    {


        Managers.UI.ClosePopupUI(this);
    }
    private void OnDisable()
    {
        Managers.Game.OnEquipInfoEnter -= ShowItemInfo;
        Managers.Game.OnEquipInfoExit -= CloseItemInfo;

    }
}

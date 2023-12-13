using TMPro;

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


    private void Start()
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
        GetText((int)Texts.BuffText).text = $"공격력 : {Managers.Data.items_Equip[id].buff_Atk} / 방어력 : {Managers.Data.items_Equip[3].buff_Def} / 최대생명력 :  {Managers.Data.items_Equip[3].buff_MaxHp}";
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

    public void OnPointerDown()
    {
        Managers.UI.ClosePopupUI(this);
    }

}

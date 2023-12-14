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
        atkText,
        maxHpText,
        defText,

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

        GetButton((int)Buttons.LeftBtn).gameObject.BindEvent(OnBtnLeftPage);
        GetButton((int)Buttons.RightBtn).gameObject.BindEvent(OnBtnRightPage);
        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnBtnCancel);

        GetObject((int)GameObjects.InfoPanel).SetActive(false);

        CreateSlot();

        Managers.Game.OnEquipInfoEnter -= ShowItemInfo;
        Managers.Game.OnEquipInfoEnter += ShowItemInfo;
        Managers.Game.OnEquipInfoExit -= CloseItemInfo;
        Managers.Game.OnEquipInfoExit += CloseItemInfo;

        Managers.Game.OnStateTextChanged -= PlayerStatTextRefresh;
        Managers.Game.OnStateTextChanged += PlayerStatTextRefresh;

        Managers.Game.OnEquipChanged?.Invoke();

        return true;
    }


    void PlayerStatTextRefresh()
    {
        GetText((int)Texts.atkText).text = $"공격력 : {Managers.Game.player.atk}";
        GetText((int)Texts.defText).text = $"방어력 : {Managers.Game.player.def}";
        GetText((int)Texts.maxHpText).text = $"체력 : {Managers.Game.player.maxHp}";

    }

    private void CreateSlot()
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

      
        GetImage((int)Images.ItemIcon).sprite = Managers.Data.items_Equip[id].IconImage;



        //  GetImage((int)Images.ItemIcon).sprite = Resources.Load<Sprite>($"Test/{Managers.Data.items_Equip[id].IconImage.name}");



        GetText((int)Texts.ItemComentText).text = Managers.Data.items_Equip[id].comment;

            GetObject((int)GameObjects.InfoPanel).SetActive(true);

    }
    public void CloseItemInfo()
    {
            GetObject((int)GameObjects.InfoPanel).SetActive(false);
    }


    public void OnBtnCancel()
    {
        Managers.UI.ClosePopupUI(this);
    }

    public void OnBtnLeftPage()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup_State>();
    }
    public void OnBtnRightPage()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup_State>();

    }
    private void OnDisable()
    {
        Managers.Game.OnEquipInfoEnter -= ShowItemInfo;
        Managers.Game.OnEquipInfoExit -= CloseItemInfo;
        Managers.Game.OnStateTextChanged -= PlayerStatTextRefresh;
    }
}

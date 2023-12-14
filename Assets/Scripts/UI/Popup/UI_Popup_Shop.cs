using UnityEngine;

public class UI_Popup_Shop : UI_Popup
{


    enum GameObjects
    {
        ShopCatalog,
        PurchasePopup,
        InfoPanel,
        Content,
    }

    enum Buttons
    {
        PurchaseBtn_Yes,
        PurchaseBtn_No,
        CancelBtn
    }

    enum Texts
    {
        ItemNameText,
        BuffText,

        ItemComentText,
        PurchaseNameText,
        PurchasePriceText

    }
    enum Images
    {
        PurchaseItemIcon
    }

    private int _selectNumber = 0;

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

        CreateSlot();

        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnBtnCancel);



        GetButton((int)Buttons.PurchaseBtn_Yes).gameObject.BindEvent(OnBtnPurchaseYes);
        GetButton((int)Buttons.PurchaseBtn_No).gameObject.BindEvent(OnBtnPurchaseNo);




        GetObject((int)GameObjects.ShopCatalog).SetActive(true);
        GetObject((int)GameObjects.PurchasePopup).SetActive(false);
        GetObject((int)GameObjects.InfoPanel).SetActive(false);

        Refresh();


        return true;
    }


    private void CreateSlot()
    {
        for (int i = 0; i < Managers.Data.items_Equip.Keys.Count; i++)
        {
            UI_Shop_ItemCard item = Managers.Resource.InstantiatePrefab("UI_Shop_ItemCard.prefab", GetObject((int)GameObjects.Content).transform).GetComponent<UI_Shop_ItemCard>();
            item.uI_Popup_Shop = this;
            item.item_Equip = Managers.Data.items_Equip[i];
            item.Refresh();
            item.PurcCheck();

        }
    }

    private void Refresh()
    {
        // 구매한 템 갱싱
    }



    public void ShowItemInfo(int id)
    {
        _selectNumber = id;

        GetText((int)Texts.ItemNameText).text = Managers.Data.items_Equip[id].name;
        GetText((int)Texts.BuffText).text =
                 ((Managers.Data.items_Equip[id].buff_Atk != 0) ? $"공격력 : {Managers.Data.items_Equip[id].buff_Atk}    " : "") +
                 ((Managers.Data.items_Equip[id].buff_Def != 0) ? $"방어력 : {Managers.Data.items_Equip[id].buff_Def}    " : "") +
                 ((Managers.Data.items_Equip[id].buff_MaxHp != 0) ? $"최대생명력 : {Managers.Data.items_Equip[id].buff_MaxHp}" : "");

        GetText((int)Texts.ItemComentText).text = Managers.Data.items_Equip[id].comment;

        GetObject((int)GameObjects.InfoPanel).SetActive(true);

    }
    public void CloseItemInfo()
    {
        GetObject((int)GameObjects.InfoPanel).SetActive(false);
    }
    public void PurchasePopupOpen(int id)
    {
        _selectNumber = id;

        GetObject((int)GameObjects.ShopCatalog).SetActive(false);
        GetObject((int)GameObjects.PurchasePopup).SetActive(true);

        GetText((int)Texts.PurchaseNameText).text = Managers.Data.items_Equip[_selectNumber].name;
        GetText((int)Texts.PurchasePriceText).text = $"{Managers.Data.items_Equip[_selectNumber].price}";
        GetImage((int)Images.PurchaseItemIcon).sprite = Managers.Data.items_Equip[_selectNumber].IconImage;

    }


    void OnBtnPurchaseYes()
    {
        if (Managers.Data.userData.gold - Managers.Data.items_Equip[_selectNumber].price < 0)
        {
            Debug.Log("돈부족");
            return;
        }

        Managers.Data.userData.gold -= Managers.Data.items_Equip[_selectNumber].price;
        Managers.Data.userData.invenGetArray[_selectNumber] = true;
        Managers.Data.SaveUserDataToJson();

        Managers.Game.OnGoldChanged?.Invoke();

        Refresh();

        GetObject((int)GameObjects.ShopCatalog).SetActive(true);
        GetObject((int)GameObjects.PurchasePopup).SetActive(false);

    }

    void OnBtnPurchaseNo()
    {
        GetObject((int)GameObjects.ShopCatalog).SetActive(true);
        GetObject((int)GameObjects.PurchasePopup).SetActive(false);
    }


    public void OnBtnCancel()
    {
        Managers.UI.ClosePopupUI(this);
    }

}

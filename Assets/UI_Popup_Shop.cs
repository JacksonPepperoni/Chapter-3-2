using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Shop : UI_Popup
{


    enum GameObjects
    {
        ShopCatalog,
        PurchasePopup,

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
        PriceText,

    }
    enum Images
    {
        ItemIcon
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

        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnBtnCancel);



        GetButton((int)Buttons.PurchaseBtn_Yes).gameObject.BindEvent(OnBtnPurchaseYes);
        GetButton((int)Buttons.PurchaseBtn_No).gameObject.BindEvent(OnBtnPurchaseNo);


        CreateSlot();


        GetObject((int)GameObjects.ShopCatalog).SetActive(true);
        GetObject((int)GameObjects.PurchasePopup).SetActive(false);


        Refresh();


        return true;
    }


    void Refresh()
    {
    }

    private void CreateSlot()
    {
        for (int i = 0; i < Managers.Data.items_Equip.Keys.Count; i++)
        {
            UI_Inven_Slot item = Managers.Resource.InstantiatePrefab("UI_Inven_Slot.prefab", GetObject((int)GameObjects.Content).transform).GetComponent<UI_Inven_Slot>();

            item.item_Equip = Managers.Data.items_Equip[i];
        }

    }

    public void PurchasePopupOpen(int id)
    {
        _selectNumber = id;

        GetObject((int)GameObjects.ShopCatalog).SetActive(false);
        GetObject((int)GameObjects.PurchasePopup).SetActive(true);

        GetText((int)Texts.ItemNameText).text = Managers.Data.items_Equip[_selectNumber].name;
        GetText((int)Texts.PriceText).text = $"{Managers.Data.items_Equip[_selectNumber].price}";
        GetImage((int)Images.ItemIcon).sprite = Managers.Data.items_Equip[_selectNumber].IconImage;
        
    }



    void OnBtnPurchaseYes()
    {

        Managers.Data.userData.gold -= Managers.Data.items_Equip[_selectNumber].price;

      Managers.Data.SaveUserDataToJson();

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

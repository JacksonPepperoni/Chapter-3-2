using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Shop_ItemCard : UI_Base
{
    enum Texts
    {
        ItemPriceText

    }
    enum Images
    {
        ItemIcon

    }
    [HideInInspector] public Item_Equip item_Equip = null;
    [HideInInspector] public UI_Popup_Shop uI_Popup_Shop;

    bool isGet = false;


    void OnEnable()
    {
        Initialize();
    }


    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindText(typeof(Texts));
        BindImage(typeof(Images));


        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonEnter, Define.UIEvent.PointerEnter);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonExit, Define.UIEvent.PointerExit);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(OnButtonClick);


        Managers.Game.OnGoldChanged -= PurcCheck;
        Managers.Game.OnGoldChanged += PurcCheck;


        return true;
    }

    public void Refresh()
    {
        GetText((int)Texts.ItemPriceText).text = $"{item_Equip.price}원";
        GetImage((int)Images.ItemIcon).sprite = item_Equip.IconImage;

    }
    public void PurcCheck()
    {
        isGet = Managers.Data.userData.invenGetArray[item_Equip.id];
        GetImage((int)Images.ItemIcon).color = isGet ? Color.gray : Color.white;
    }


    void OnButtonClick()
    {
        if (isGet) return;

        uI_Popup_Shop.PurchasePopupOpen(item_Equip.id);
    }

    public void OnButtonEnter(BaseEventData data)
    {
        uI_Popup_Shop.ShowItemInfo(item_Equip.id);

    }
    public void OnButtonExit(BaseEventData data)
    {
        uI_Popup_Shop.CloseItemInfo();
    }

    private void OnDisable()
    {
        Managers.Game.OnGoldChanged -= PurcCheck;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_State : UI_Popup
{
    enum GameObjects
    {
       
    }
    enum Texts
    {
        atkText,
        defText,
        maxHpText,

    }

    enum Buttons
    {
        LeftBtn,
        RightBtn,
        CancelBtn
    }

    private int _maxInvenLength = 10;


    private void OnEnable()
    {
        Initialize();


        TextUpdate();

    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;


        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        


        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnPointerDown);

        GetButton((int)Buttons.RightBtn).gameObject.BindEvent(OnBtnRight);
        GetButton((int)Buttons.LeftBtn).gameObject.BindEvent(OnBtnLeft);


        return true;
    }

    void TextUpdate()
    {

        GetText((int)Texts.atkText).text = $"공격력 : {Managers.Game.player.atk}";
        GetText((int)Texts.defText).text = $"방어력 : {Managers.Game.player.def}";
        GetText((int)Texts.maxHpText).text = $"체력 : {Managers.Game.player.maxHp}";


    }

    public void OnPointerDown()
    {
        Managers.UI.ClosePopupUI(this);

    }

    public void OnBtnLeft()
    {
        Managers.UI.ClosePopupUI(this);
   Managers.UI.ShowPopupUI<UI_Popup_Inven>();

    }
    public void OnBtnRight()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup_Inven>();

    }

}

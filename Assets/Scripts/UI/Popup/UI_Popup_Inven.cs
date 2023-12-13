using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Inven : UI_Popup
{
    enum GameObjects
    {
       
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
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;


        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));


        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnPointerDown);



        GetButton((int)Buttons.RightBtn).gameObject.BindEvent(OnBtnRight);
        GetButton((int)Buttons.LeftBtn).gameObject.BindEvent(OnBtnLeft);

        for (int i = 0; i < _maxInvenLength; i++)
        {

   
        }

        return true;
    }

    public void OnPointerDown()
    {
        Managers.UI.ClosePopupUI(this);

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

}

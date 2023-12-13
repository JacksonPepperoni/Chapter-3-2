using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Scene_Game : UI_Scene
{

    enum Buttons
    {
        InvenBtn,
        StateBtn,
        OptionBtn

    }

    void Start()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));



        GetButton((int)Buttons.InvenBtn).gameObject.BindEvent(OnPointerDownInven);

        GetButton((int)Buttons.OptionBtn).gameObject.BindEvent(OnPointerDownOption);
        GetButton((int)Buttons.StateBtn).gameObject.BindEvent(OnPointerDownState);
        return true;
    }

    void OnPointerDownInven()
    {
        Managers.UI.ShowPopupUI<UI_Popup_Inven>();

    }
    void OnPointerDownState()
    {
        Managers.UI.ShowPopupUI<UI_Popup_State>();

    }
    void OnPointerDownOption()
    {
        Managers.UI.ShowPopupUI<UI_Popup_Option>();

    }
}

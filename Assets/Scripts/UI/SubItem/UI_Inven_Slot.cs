using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Slot : UI_Base
{
    enum Buttons
    {
        UseBtn,
        CancelBtn

    }

    enum Texts
    {
        CapacityText,
    }

    enum GameObjects
    {
        CheckPanel
    }

    enum Images
    {
        ItemIcon,
    }

    int capacity = 0;

    void OnEnable()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));


        GetObject((int)GameObjects.CheckPanel).SetActive(false);

        GetText((int)Texts.CapacityText).text = $"{capacity}";


        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonEnter, Define.UIEvent.PointerEnter);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonExit, Define.UIEvent.PointerExit);

        GetImage((int)Images.ItemIcon).gameObject.BindEvent(OnButtonClick);



        GetButton((int)Buttons.UseBtn).gameObject.BindEvent(Use);
        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(Cancel);

        return true;
    }

    void OnButtonClick()
    {
        _UI_Popup_Tooltip.Hide(null);
        GetObject((int)GameObjects.CheckPanel).SetActive(true);
    }

    void Use()
    {
        Debug.Log("사용했습니다");
        GetObject((int)GameObjects.CheckPanel).SetActive(false);
    }

    void Cancel()
    {
        GetObject((int)GameObjects.CheckPanel).SetActive(false);
    }



    private UI_Popup_Tooltip _UI_Popup_Tooltip;


    public void OnButtonEnter(BaseEventData data)
    {
        _UI_Popup_Tooltip = Managers.UI.ShowPopupUI<UI_Popup_Tooltip>();
        _UI_Popup_Tooltip.Show(this.name, this.name + "입니다 실험중 테스트 테스트 야호야호");


    }
    public void OnButtonExit(BaseEventData data)
    {
        _UI_Popup_Tooltip.Hide(data);

    }


}

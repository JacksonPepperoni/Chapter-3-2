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

    }

    enum Texts
    {
        CapacityText,
    }

    enum GameObjects
    {
    }

    enum Images
    {
        Highlight_Front,
        Highlight_Back,
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


        GetText((int)Texts.CapacityText).text = $"{capacity}";


        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonEnter, Define.UIEvent.PointerEnter);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonExit, Define.UIEvent.PointerExit);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(OnButtonClick);


        GetImage((int)Images.Highlight_Back).gameObject.SetActive(false);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(false);



        return true;
    }

    void OnButtonClick()
    {

    }

    public void OnButtonEnter(BaseEventData data)
    {

        GetImage((int)Images.Highlight_Back).gameObject.SetActive(true);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(true);

    }
    public void OnButtonExit(BaseEventData data)
    {
        GetImage((int)Images.Highlight_Back).gameObject.SetActive(false);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(false);

    }


}

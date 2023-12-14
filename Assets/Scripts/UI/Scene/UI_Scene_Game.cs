using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Scene_Game : UI_Scene
{


    enum Images
    {


    }
    enum GameObjects
    {
        HpPanel
    }

    enum Buttons
    {
        TestShop,

        StateBtn,
        InvenBtn,
        OptionBtn

    }
    enum Texts
    {
        GoldText

    }

    int maxHeartCount = 10;
    void OnEnable()
    {
        Initialize();
    }

    private void Start()
    {
        HealthRefresh();
        GoldRefresh();


    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindObject(typeof(GameObjects));
        BindImage(typeof(Images));


        GetButton((int)Buttons.InvenBtn).gameObject.BindEvent(OnBtnInven);
        GetButton((int)Buttons.StateBtn).gameObject.BindEvent(OnBtnState);
        GetButton((int)Buttons.OptionBtn).gameObject.BindEvent(OnBtnOption);

        GetButton((int)Buttons.TestShop).gameObject.BindEvent(Testtt);

        for (int i = 0; i < maxHeartCount; i++)
        {
            Managers.Resource.InstantiatePrefab("HpBlock.prefab", GetObject((int)GameObjects.HpPanel).transform).SetActive(false);
        }

        Managers.Game.OnGoldChanged -= GoldRefresh;
        Managers.Game.OnGoldChanged += GoldRefresh;

        Managers.Game.HpBarOnly -= HealthRefresh;
        Managers.Game.HpBarOnly += HealthRefresh;

    


        return true;
    }

    void GoldRefresh()
    {
        GetText((int)Texts.GoldText).text = $"{Managers.Data.userData.gold}원";
    }

    void HealthRefresh()
    {
       

        for (int i = 0; i < maxHeartCount; i++)
        {
            GetObject((int)GameObjects.HpPanel).transform.GetChild(i).gameObject.SetActive(false);
        }


        for (int i = 0; i < Managers.Data.userData.maxHp; i++)
        {
            GetObject((int)GameObjects.HpPanel).transform.GetChild(i).gameObject.SetActive(true);
        }

    }


    void OnBtnInven()
    {
        Managers.UI.ShowPopupUI<UI_Popup_Inven>();

    }
    void OnBtnState()
    {
        Managers.UI.ShowPopupUI<UI_Popup_State>();

    }
    void OnBtnOption()
    {
        Managers.UI.ShowPopupUI<UI_Popup_Option>();

    }

    void Testtt()
    {
        Managers.UI.ShowPopupUI<UI_Popup_Shop>();

    }

    private void OnDisable()
    {
        Managers.Game.HpBarOnly -= HealthRefresh;
        Managers.Game.OnGoldChanged -= GoldRefresh;
    }
}

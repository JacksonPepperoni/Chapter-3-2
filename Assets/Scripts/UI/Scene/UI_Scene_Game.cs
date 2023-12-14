public class UI_Scene_Game : UI_Scene
{

  
    enum Images
    {
      

    }
    enum GameObjects
    {
        Character,
        Cursor,
    }

    enum Buttons
    {
        InvenBtn,
        StateBtn,
        OptionBtn

    }
    enum Texts
    {
       GoldText

    }
    void OnEnable()
    {
        Initialize();
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



        GetText((int)Texts.GoldText).text = $"{Managers.Data.userData.gold}원";


        return true;
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
}

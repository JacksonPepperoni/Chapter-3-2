
public class UI_Popup_State : UI_Popup
{
    enum Texts
    {
        NameText
    }

    enum Buttons
    {
        LeftBtn,
        RightBtn,
        CancelBtn
    }

    private void OnEnable()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));
        BindText(typeof(Texts));


        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnBtnCancel);

        GetButton((int)Buttons.RightBtn).gameObject.BindEvent(OnBtnLeftPage);
        GetButton((int)Buttons.LeftBtn).gameObject.BindEvent(OnBtnRightPage);

        return true;
    }

    public void OnBtnCancel()
    {
        Managers.UI.ClosePopupUI(this);
    }

    public void OnBtnLeftPage()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup_Inven>();
    }
    public void OnBtnRightPage()
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.ShowPopupUI<UI_Popup_Inven>();

    }

}

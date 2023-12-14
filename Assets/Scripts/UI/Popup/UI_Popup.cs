public class UI_Popup : UI_Base
{

    public override bool Initialize()
    {
        if (base.Initialize() == false) return false;

        Managers.UI.SetCanvas(this.gameObject, true);

        return true;
    }

    public virtual void ClosePopupUI()
    {
        Managers.UI.ClosePopupUI(this);
    }

}
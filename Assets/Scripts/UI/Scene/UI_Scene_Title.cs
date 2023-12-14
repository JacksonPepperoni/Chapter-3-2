public class UI_Scene_Title : UI_Scene
{

    enum Buttons
    {
        StartBtn
    }


    void Start()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.StartBtn).gameObject.BindEvent(OnButtonStart);



        return true;
    }

    private void OnButtonStart()
    {
        Managers.Scene.LoadScene("GameScene");
    }
}
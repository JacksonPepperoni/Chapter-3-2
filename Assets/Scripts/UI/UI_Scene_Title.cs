using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UI_Scene_Title : UI_Scene
{
    #region Enums

    enum Buttons
    {
      
    }

    #endregion

    void Start()
    {
        Initialize();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));

    //    GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnButtonStart);

        return true;
    }

    private void OnButtonStart(PointerEventData data)
    {
    }
}
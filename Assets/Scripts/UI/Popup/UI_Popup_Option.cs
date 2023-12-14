using UnityEngine;
using UnityEngine.EventSystems;

public class UI_Popup_Option : UI_Popup
{
    enum Buttons
    {
        CancelBtn
    }

    enum GameObjects
    {

        BtnPanel
    }

    enum Texts
    {

        NameText
    }

    private Animator _anim;


    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Initialize();

    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindButton(typeof(Buttons));
        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));


        GetText((int)Texts.NameText).gameObject.BindEvent(null, OnPointerDown, Define.UIEvent.PointerDown);
        //  GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(null, OnPointerDown, Define.UIEvent.PointerDown);

        _anim.SetBool("isOpen", true);

        return true;

    }

    public void OnPointerDown(BaseEventData data)
    {
        Debug.Log("클릭");

        _anim.SetBool("isOpen", false);
       
        Managers.UI.ClosePopupUI(this);
    }



}

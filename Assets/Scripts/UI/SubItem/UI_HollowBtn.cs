using UnityEngine;
using UnityEngine.EventSystems;

public class UI_HollowBtn : UI_Base
{

    enum Texts
    {
        NameText
    }
    enum Images
    {

    }
    enum Buttons
    {
        ClickBtn
    }
    enum GameObjects
    {

    }

    // 부모 받기


    private static readonly int hashIsOn = Animator.StringToHash("isOn");
    private static readonly int hashClick = Animator.StringToHash("Click");


    [SerializeField] private string _menuName;

    private RectTransform _rec;
    private Animator _anim;

    void Start()
    {
        _rec = GetComponent<RectTransform>();
        _anim = GetComponent<Animator>();


        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));


        GetText((int)Texts.NameText).text = _menuName;

        _rec.sizeDelta = new Vector2(GetText((int)Texts.NameText).preferredWidth + 45, GetText((int)Texts.NameText).preferredHeight + 25); //버튼 클릭범위가 딱 글씨칸만되서 여분더하기


        GetText((int)Texts.NameText).gameObject.BindEvent(null, OnPointerEnter, Define.UIEvent.PointerEnter);
        GetText((int)Texts.NameText).gameObject.BindEvent(null, OnPointerExit, Define.UIEvent.PointerExit);
        GetText((int)Texts.NameText).gameObject.BindEvent(OnPointerDown);

      //  GetButton((int)Buttons.ClickBtn).gameObject.BindEvent(null, OnPointerEnter, Define.UIEvent.PointerEnter);
     //   GetButton((int)Buttons.ClickBtn).gameObject.BindEvent(null, OnPointerExit, Define.UIEvent.PointerExit);
    //    GetButton((int)Buttons.ClickBtn).gameObject.BindEvent(OnPointerDown);

    }



    public void OnPointerEnter(BaseEventData data)
    {
        _anim.SetBool(hashIsOn, true);
    }

    public void OnPointerExit(BaseEventData data)
    {
        _anim.SetBool(hashIsOn, false);
    }

    public void OnPointerDown()
    {

        if (!_anim.GetBool(hashIsOn)) return;


        _anim.SetTrigger(hashClick);
        _anim.SetBool(hashIsOn, false);


        Managers.UI.CloseAllPopupUI(); // 특단의조치

    }



}

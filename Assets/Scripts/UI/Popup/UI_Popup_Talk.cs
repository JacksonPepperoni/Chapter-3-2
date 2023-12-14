using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Popup_Talk : UI_Popup
{

    enum Texts
    {
        LineText,
        NameText

    }
    enum Images
    {
        Character,
        NameImage,
        LineImage

    }
    enum Buttons
    {
        TalkBtn,
    }
    enum GameObjects
    {
        Character,
        Cursor,
    }

    public enum Dialogue
    {
        WAVE,
        CLEAR,
        FAIL
    }


    private Animator _anim;
    private Animator _cursorAnim;


    private bool _isTyping = false;
    private bool _isClicking; //커서 눌리는 애니 동안 입력안받게

    private float _clickDelay;

    private int _line;
    private WaitForSeconds _typingSpeed = new WaitForSeconds(0.02f); // 타이핑 속도


    private Dictionary<int, DialogueSetting> talkData;
    private Dictionary<Dialogue, Dictionary<int, DialogueSetting>> mWJ = new Dictionary<Dialogue, Dictionary<int, DialogueSetting>>();



    //  대화창 호출방법 :  Main.UI.ShowPopupUI<UI_Popup_Talk>().DialogueOpen(StageCharge.MWJ, UI_Popup_Talk.Dialogue.FAIL);


    public override bool Initialize()
    {
        if (!base.Initialize()) return false;


        BindObject(typeof(GameObjects));
        BindButton(typeof(Buttons));
        BindText(typeof(Texts));
        BindImage(typeof(Images));


        _anim = GetComponent<Animator>();


        GetButton((int)Buttons.TalkBtn).gameObject.BindEvent(Talk);

        _cursorAnim = GetObject((int)GameObjects.Cursor).GetComponent<Animator>();
        _clickDelay = 0.3f; // 클릭 애니클립 길이 읽고 넣어주기

        return true;
    }

    private void Start()
    {
        _cursorAnim.gameObject.SetActive(false);
        _isClicking = false;
        _isTyping = false;
        GetText((int)Texts.LineText).text = "";
        _line = 0;

        NameChange(talkData[0].name);

        _anim.SetBool("isOpen", true);

        Talk();

    }



    public void Talk()
    {
        if (_isClicking) return;


        if (_isTyping)  //대사 흐르는중에 클릭하면 대사 다뜸
        {
            CancelInvoke();
            StopAllCoroutines();
            GetText((int)Texts.LineText).maxVisibleCharacters = 999;
            _line += 1;

            _isTyping = false;

            _cursorAnim.gameObject.SetActive(true);

            return;
        }

        _isClicking = true; // 커서클릭 애니 진행중
        _cursorAnim.SetTrigger("Click");
        Invoke("TalkEvent", _clickDelay); // 커서 클릭애니 끝난후에 실행

    }




    public void TalkEvent()
    {
        _isClicking = false;

        if (!talkData.ContainsKey(_line))
        {
            Close();
            return;
        }

        _cursorAnim.gameObject.SetActive(false);


        if (GetText((int)Texts.NameText).text != talkData[_line].name)
            NameChange(talkData[_line].name);


        GetText((int)Texts.LineText).text = talkData[_line].line;

        StartCoroutine(TextVisible());
    }


    private IEnumerator TextVisible()
    {
        _isTyping = true;

        GetText((int)Texts.LineText).ForceMeshUpdate();

        GetText((int)Texts.LineText).maxVisibleCharacters = 0;
        yield return new WaitForSeconds(0.02f); // 첫대사 안뜨는 오류 잡는용


        int totalVisibleCharacters = GetText((int)Texts.LineText).textInfo.characterCount;
        int counter = 0;


        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            GetText((int)Texts.LineText).maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters) // 현재줄 대사 다 나왔으면
            {
                _line += 1;
                _isTyping = false;
                _cursorAnim.gameObject.SetActive(true);
                break;
            }

            counter += 1;
            yield return _typingSpeed;
        }
    }



    public void NameChange(string name) // 이름에 맞게 이름칸 사이즈 늘리기
    {
        GetText((int)Texts.NameText).text = name;
        GetImage((int)Images.NameImage).rectTransform.sizeDelta = new Vector2(GetText((int)Texts.NameText).preferredWidth + 120f, GetImage((int)Images.NameImage).rectTransform.sizeDelta.y);

    }



    public void Close()
    {
        StopAllCoroutines();
        CancelInvoke();
        _isTyping = false;

        _anim.SetBool("isOpen", false);

        Invoke("CloseEvent", _anim.GetCurrentAnimatorStateInfo(0).length); // 대화창 다 내려가면 UI사라지도록
    }

    public void CloseEvent()
    {
        Managers.UI.ClosePopupUI(this);
    }

}

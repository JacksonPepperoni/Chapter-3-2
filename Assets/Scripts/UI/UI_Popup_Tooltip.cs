using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public class UI_Popup_Tooltip : UI_Popup
{

    enum GameObjects
    {
        Tooltip,
        Header,
        Content
    }
    enum Texts
    {
        Header,
        Content
    }


    private RectTransform _rectTransform;

    private CanvasGroup _canvasGroup;

    private Vector2 _position;
    private float _pivotX;
    private float _pivotY;


    private void OnEnable()
    {

        Initialize();

    }


    void LateUpdate()
    {

        MovePosition();
    }

    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        BindObject(typeof(GameObjects));
        BindText(typeof(Texts));

        _rectTransform = GetObject((int)GameObjects.Tooltip).gameObject.GetComponent<RectTransform>();
        _canvasGroup = GetObject((int)GameObjects.Tooltip).gameObject.GetComponent<CanvasGroup>();

        GetObject((int)GameObjects.Tooltip).SetActive(false);

        _position = Input.mousePosition;


        return true;
    }


    void MovePosition()
    {
        _position = Input.mousePosition;

        _pivotX = _position.x / Screen.width;
        _pivotY = _position.y / Screen.height;

        _rectTransform.pivot = new Vector2(_pivotX, _pivotY);
        GetObject((int)GameObjects.Tooltip).transform.position = _position;
    }

    public void Show(string header, string content)
    {
        _canvasGroup.alpha = 0;
        _position = Input.mousePosition;
        GetObject((int)GameObjects.Tooltip).SetActive(true);
        _canvasGroup.DOFade(1, 0.3f);

        GetText((int)Texts.Header).text = header;
        GetText((int)Texts.Content).text = content;

    }

    public void Hide(BaseEventData data)
    {
        //_canvasGroup.DOFade(0, 0.2f);
        _canvasGroup.DOKill(); // dotween 작동 다하기전에 물체 사라졌다는 알람때문에 추가

        Managers.UI.ClosePopupUI(this);
    }


}


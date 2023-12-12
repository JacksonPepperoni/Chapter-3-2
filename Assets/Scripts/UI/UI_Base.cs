using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Define;

public abstract class UI_Base : MonoBehaviour
{
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    private bool initialized = false;


    void Start()
    {
        Initialize();
    }
    public virtual bool Initialize()
    {
        if (initialized) return false;
        initialized = true;
        return true;
    }

    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objects);

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objects[i] = Util.FindChild(gameObject, names[i], true);
            else
                objects[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objects[i] == null)
                Debug.Log($"Failed to bind({names[i]})");
        }
    }


    protected void BindObject(Type type) => Bind<GameObject>(type);
    protected void BindImage(Type type) => Bind<Image>(type);
    protected void BindText(Type type) => Bind<TextMeshProUGUI>(type);
    protected void BindButton(Type type) => Bind<Button>(type);


    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }

    protected GameObject GetObject(int idx) { return Get<GameObject>(idx); }
    protected TextMeshProUGUI GetText(int idx) { return Get<TextMeshProUGUI>(idx); }
    protected Button GetButton(int idx) { return Get<Button>(idx); }
    protected Image GetImage(int idx) { return Get<Image>(idx); }

    public static void BindEvent(GameObject go, Action action = null, Action<BaseEventData> eventAction = null, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case UIEvent.Pressed:
                evt.OnPressedHandler -= action;
                evt.OnPressedHandler += action;
                break;
            case UIEvent.PointerDown:
                evt.OnPointerDownHandler -= eventAction;
                evt.OnPointerDownHandler += eventAction;
                break;
            case UIEvent.PointerUp:
                evt.OnPointerUpHandler -= eventAction;
                evt.OnPointerUpHandler += eventAction;
                break;
            case UIEvent.Drag:
                evt.OnDragHandler -= eventAction;
                evt.OnDragHandler += eventAction;
                break;
            case UIEvent.BeginDrag:
                evt.OnBeginDragHandler -= eventAction;
                evt.OnBeginDragHandler += eventAction;
                break;
            case UIEvent.EndDrag:
                evt.OnEndDragHandler -= eventAction;
                evt.OnEndDragHandler += eventAction;
                break;
            case UIEvent.PointerEnter:
                evt.OnPointerEnterHandler -= eventAction;
                evt.OnPointerEnterHandler += eventAction;
                break;
            case UIEvent.PointerExit:
                evt.OnPointerExitHandler -= eventAction;
                evt.OnPointerExitHandler += eventAction;
                break;
        }
    }
}

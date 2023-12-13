using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UI_Popup_Option : UI_Popup
{
    enum Buttons
    {
        CancelBtn
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

        GetButton((int)Buttons.CancelBtn).gameObject.BindEvent(OnPointerDown);

        _anim.SetBool("isOpen", true);

        return true;

    }

    public void OnPointerDown()
    {
        Debug.Log("Å¬¸¯");
        _anim.SetBool("isOpen", false);
        Managers.UI.ClosePopupUI(this);

    }



}

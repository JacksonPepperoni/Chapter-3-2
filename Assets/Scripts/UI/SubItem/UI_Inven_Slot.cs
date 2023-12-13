using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Inven_Slot : UI_Base
{
    enum Buttons
    {

    }

    enum Texts
    {
    }

    enum GameObjects
    {
        EquipCheck
    }

    enum Images
    {
        Highlight_Front,
        Highlight_Back,
        ItemIcon,
    }

    [HideInInspector] public Item_Equip item_Equip = null;

    [HideInInspector] public int slotNumber;
    void Start()
    {
        Initialize();

    }


    public override bool Initialize()
    {
        if (!base.Initialize()) return false;

        Bind<Button>(typeof(Buttons));
        Bind<TextMeshProUGUI>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonEnter, Define.UIEvent.PointerEnter);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(null, OnButtonExit, Define.UIEvent.PointerExit);
        GetImage((int)Images.ItemIcon).gameObject.BindEvent(OnButtonClick);

        GetImage((int)Images.Highlight_Back).gameObject.SetActive(false);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(false);

        Refresh();

        Managers.Game.OnEquipChanged -= Refresh;
        Managers.Game.OnEquipChanged += Refresh;

        return true;
    }

    void Refresh()
    {
        GetImage((int)Images.ItemIcon).color = (Managers.Data.userData.invenGetArray[slotNumber]) ? Color.white : Color.gray;
        GetObject((int)GameObjects.EquipCheck).SetActive(Managers.Data.userData.isWearArray[slotNumber]);


    }



    void OnButtonClick()
    {
        if (!Managers.Data.userData.invenGetArray[slotNumber]) return;

        if (Managers.Data.userData.isWearArray[slotNumber])
            UnEquip();
        else
            Equip();
    }


    public void Equip()
    {
        Managers.Data.userData.isWearArray[slotNumber] = true;
        Refresh();
        Managers.Data.SaveUserDataToJson();


        Managers.Game.OnEquipChanged();

        Debug.Log($"{Managers.Data.items_Equip[slotNumber].name} ¿Â¬¯øœ∑·");
    }

    public void UnEquip()
    {
        Managers.Data.userData.isWearArray[slotNumber] = false;
        Refresh();
        Managers.Data.SaveUserDataToJson();
        Managers.Game.OnEquipChanged();
        Debug.Log($"{Managers.Data.items_Equip[slotNumber].name} ¿Â¬¯«ÿ¡¶");
    }



    public void OnButtonEnter(BaseEventData data)
    {
        GetImage((int)Images.Highlight_Back).gameObject.SetActive(true);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(true);

        Managers.Game.OnEquipInfoEnter(item_Equip.id);

    }
    public void OnButtonExit(BaseEventData data)
    {
        GetImage((int)Images.Highlight_Back).gameObject.SetActive(false);
        GetImage((int)Images.Highlight_Front).gameObject.SetActive(false);

        Managers.Game.OnEquipInfoExit();
    }
}

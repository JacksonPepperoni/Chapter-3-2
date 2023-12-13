using System.Collections.Generic;

public class DataManager
{

    public Dictionary<int, Item_Equip> items_Equip = new();
    List<Dictionary<string, object>> _csv_Item_Equip;

    private bool _Initialized = false;


    public bool Initialize()
    {
        if (_Initialized) return false;

        _csv_Item_Equip = CSVReader.Read("ItemData");

        for (int i = 0; i < _csv_Item_Equip.Count; i++) //CSV받아온거 형변환하고 클래스에 넣기 장비 csv넣기
        {
            Item_Equip item = new();

            item.Initialize((int)_csv_Item_Equip[i]["ID"],
                   _csv_Item_Equip[i]["이름"].ToString(),
                   _csv_Item_Equip[i]["설명"].ToString(),
                   (int)_csv_Item_Equip[i]["가격"],
                   null); ; // 이미지 어드레서블에서 불러와서 넣기  csv_Item_Equip[i]["아이콘파일명]


            item.part = ((Define.EquipParts)((int)_csv_Item_Equip[i]["부위"]));
            item.buff_MaxHp = (int)_csv_Item_Equip[i]["최대생명력"];
            item.buff_Atk = (int)_csv_Item_Equip[i]["공격력"];
            item.buff_Def = (int)_csv_Item_Equip[i]["방어력"];

            items_Equip.Add(item.id, item);
        }


        _Initialized = true;
        return true;
    }

}

// int는 null이 없다

//_csv_Item_Equip[i]["설명"].ToString().Replace("<br>", "\n"), // 내가 엔터적은 부분을 진짜 엔터로 바꾸기   // 개행문자를 \n대신 <br>로 사용한다.
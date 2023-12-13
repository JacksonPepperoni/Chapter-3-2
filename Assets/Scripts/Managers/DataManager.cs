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

        for (int i = 0; i < _csv_Item_Equip.Count; i++) //CSV�޾ƿ°� ����ȯ�ϰ� Ŭ������ �ֱ� ��� csv�ֱ�
        {
            Item_Equip item = new();

            item.Initialize((int)_csv_Item_Equip[i]["ID"],
                   _csv_Item_Equip[i]["�̸�"].ToString(),
                   _csv_Item_Equip[i]["����"].ToString(),
                   (int)_csv_Item_Equip[i]["����"],
                   null); ; // �̹��� ��巹������ �ҷ��ͼ� �ֱ�  csv_Item_Equip[i]["���������ϸ�]


            item.part = ((Define.EquipParts)((int)_csv_Item_Equip[i]["����"]));
            item.buff_MaxHp = (int)_csv_Item_Equip[i]["�ִ�����"];
            item.buff_Atk = (int)_csv_Item_Equip[i]["���ݷ�"];
            item.buff_Def = (int)_csv_Item_Equip[i]["����"];

            items_Equip.Add(item.id, item);
        }


        _Initialized = true;
        return true;
    }

}

// int�� null�� ����

//_csv_Item_Equip[i]["����"].ToString().Replace("<br>", "\n"), // ���� �������� �κ��� ��¥ ���ͷ� �ٲٱ�   // ���๮�ڸ� \n��� <br>�� ����Ѵ�.
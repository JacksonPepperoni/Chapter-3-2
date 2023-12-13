using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class DataManager
{

    public Dictionary<int, Item_Equip> items_Equip = new();
    List<Dictionary<string, object>> _csv_Item_Equip;

    private bool _Initialized = false;


    public UserData userData = new UserData();


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
                   null); ; // �̹��� ��巡������ �ҷ��ͼ� �ֱ�  csv_Item_Equip[i]["���������ϸ�]


            item.part = ((Define.EquipParts)((int)_csv_Item_Equip[i]["����"]));
            item.buff_MaxHp = (int)_csv_Item_Equip[i]["�ִ�����"];
            item.buff_Atk = (int)_csv_Item_Equip[i]["���ݷ�"];
            item.buff_Def = (int)_csv_Item_Equip[i]["����"];

            items_Equip.Add(item.id, item);
        }

        UserDataLoad();

        _Initialized = true;
        return true;
    }



    public void UserDataLoad() //�������� �ҷ����� (���� �ִ��� ������ Ȯ���ϰ� ������ �װɷ� ������ �⺻������. ���� ������ �ҷ������ �̰ɷ� �ؾ��Ѵ�)
    {
        string path = Path.Combine(Application.dataPath + "/UserData.json");
        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists) //���� ������(true), ������(false)
        {
            Debug.Log("�����ֽ��ϴ�");
            LoadUserDataFromJson();
        }
        else
        {
            DefaultUserData();
        }
    }

    public void SaveUserDataToJson()
    {
        string jsonData = JsonUtility.ToJson(userData, true); //JsonUtility.ToJson(userData); �ϸ� ��� �����Ͱ� 1�ٷ� ����ǰ� true���̸� ����� �������ϰ� ���͵�
        string path = Path.Combine(Application.dataPath + "/UserData.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadUserDataFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/UserData.json"); //���� �߰��ϸ� PC���� ���̺�ȵȴ� �� �⺻��ο� �����ؾ��ҵ�
        string jsonData = File.ReadAllText(path);
        userData = JsonUtility.FromJson<UserData>(jsonData);
    }


    public void DefaultUserData()
    {
        for (int i = 0; i < userData.invenGetArray.Length; i++)
        {
            userData.invenGetArray[i] = false;
            userData.isWearArray[i] = false;
        }

        SaveUserDataToJson();
    }
}
// int�� null�� ����

//_csv_Item_Equip[i]["����"].ToString().Replace("<br>", "\n"), // ���� �������� �κ��� ��¥ ���ͷ� �ٲٱ�   // ���๮�ڸ� \n��� <br>�� ����Ѵ�.


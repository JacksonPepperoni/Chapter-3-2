using System.Collections.Generic;
using System.IO;
using UnityEngine;

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




        for (int i = 0; i < _csv_Item_Equip.Count; i++) //CSV받아온거 형변환하고 클래스에 넣기 장비 csv넣기
        {
            Item_Equip item = new();

            item.Initialize((int)_csv_Item_Equip[i]["ID"],
                   _csv_Item_Equip[i]["이름"].ToString(),
                   _csv_Item_Equip[i]["설명"].ToString(),
                   (int)_csv_Item_Equip[i]["가격"],
               Resources.Load<Sprite>($"Test/{_csv_Item_Equip[i]["이미지파일명"].ToString()}"));



            //Managers.Resource.Load<Sprite>(_csv_Item_Equip[i]["이미지파일명"].ToString() +".png"));
            //  null);

            //  Addressables.LoadAssetAsync<Sprite>($"{_csv_Item_Equip[i]["이미지파일명"].ToString()}.png").Result); 


            //    Debug.Log(Managers.Resource.Load<Sprite>(_csv_Item_Equip[i]["이미지파일명"].ToString() + ".png"));
            //            Debug.Log(Addressables.LoadAssetAsync<Sprite>($"{_csv_Item_Equip[i]["이미지파일명"].ToString()}.png").Result);


            // 이미지 어드래서블에서 불러와서 넣기  

            item.part = ((Define.EquipParts)((int)_csv_Item_Equip[i]["부위"]));
            item.buff_MaxHp = (int)_csv_Item_Equip[i]["최대생명력"];
            item.buff_Atk = (int)_csv_Item_Equip[i]["공격력"];
            item.buff_Def = (int)_csv_Item_Equip[i]["방어력"];

            items_Equip.Add(item.id, item);
        }

        UserDataLoad();

        _Initialized = true;
        return true;
    }



    public void UserDataLoad() //저장파일 불러오기 (파일 있는지 없는지 확인하고 있으면 그걸로 없으면 기본데이터. 게임 데이터 불러오기는 이걸로 해야한다)
    {
        string path = Path.Combine(Application.dataPath + "/UserData.json");
        FileInfo fileInfo = new FileInfo(path);

        if (fileInfo.Exists) //파일 있을때(true), 없으면(false)
        {
            Debug.Log("파일있습니다");
            LoadUserDataFromJson();
        }
        else
        {
            DefaultUserData();
        }
    }

    public void SaveUserDataToJson()
    {
        string jsonData = JsonUtility.ToJson(userData, true); //JsonUtility.ToJson(userData); 하면 모든 데이터가 1줄로 저장되고 true붙이면 사람이 보기편하게 엔터들어감
        string path = Path.Combine(Application.dataPath + "/UserData.json");
        File.WriteAllText(path, jsonData);
    }

    public void LoadUserDataFromJson()
    {
        string path = Path.Combine(Application.dataPath + "/UserData.json"); //폴더 추가하면 PC에서 세이브안된다 걍 기본경로에 저장해야할듯
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
// int는 null이 없다

//_csv_Item_Equip[i]["설명"].ToString().Replace("<br>", "\n"), // 내가 엔터적은 부분을 진짜 엔터로 바꾸기   // 개행문자를 \n대신 <br>로 사용한다.


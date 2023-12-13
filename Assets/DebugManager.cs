using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
   
    void Start()
    {
        
    }

   
    public void ADDTEST() // 인벤에 랜덤으로 아이템 추가하는 버튼
    {
        
        Managers.Game.player.Inven_Add(Random.Range(0, Managers.Data.items_Equip.Keys.Count));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
   
    void Start()
    {
        
    }

   
    public void ADDTEST() // �κ��� �������� ������ �߰��ϴ� ��ư
    {
        
        Managers.Game.player.Inven_Add(Random.Range(0, Managers.Data.items_Equip.Keys.Count));
    }
}

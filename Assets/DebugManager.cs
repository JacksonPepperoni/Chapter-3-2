using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void ADDTEST()
    {
        
        Managers.Game.player.Inven_Add(Random.Range(0, Managers.Data.items_Equip.Keys.Count));
    }
}

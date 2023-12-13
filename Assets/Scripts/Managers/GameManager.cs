using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{

    public Action OnEquipChanged;

    public Action<int> OnEquipInfoEnter;
    public Action OnEquipInfoExit;

    public Player player;

    public void Initialize()
    {
    }


    public void SaveGame()
    {
    }

    public bool LoadGame()
    {
        return true;
    }
}

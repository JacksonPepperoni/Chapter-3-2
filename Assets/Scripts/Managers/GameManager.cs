using System;
public class GameManager
{

    public Action OnEquipChanged;
    public Action OnStateTextChanged;


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

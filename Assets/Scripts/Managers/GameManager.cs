using System;
public class GameManager
{

     public Action OnGoldChanged;

    //  public Action OnEquipmentChanged;

    
    public Action OnEquipChanged;

    public Action HpBarOnly;

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

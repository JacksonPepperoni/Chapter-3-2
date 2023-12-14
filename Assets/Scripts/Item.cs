
using UnityEngine;

public class Item
{

    public int id;
    public string name;
    public string comment;
    public int price;

    public Sprite IconImage;

    public virtual void Initialize(int idd, string namee, string comee, int pricc, Sprite img)
    {
        id = idd;
        name = namee;
        comment = comee;
        price = pricc;
        IconImage = img;
    }

}

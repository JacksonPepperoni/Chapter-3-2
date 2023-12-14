using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{

    enum Name
    {
        slug,
        plant
    }
    [SerializeField] Name name;


    Dictionary<Name, string> npcNames = new Dictionary<Name, string>
    {
    { Name.plant, "충식이" },
    { Name.slug, "에스카르고" },
     };


    [HideInInspector] public Dictionary<int, DialogueSetting> talkData;
    [SerializeField] GameObject signal;

    void Awake()
    {
        if (name == Name.plant)
        {
            talkData = new Dictionary<int, DialogueSetting>
            {
                //      { 0, new DialogueSetting(npcNames[name], "너는 파리 먹을 때 뭘 뿌려서 먹니?", 0.02f) },
                //      { 1, new DialogueSetting(npcNames[name], "뭐? 파리를 안 먹는다고?!", 0.02f) },
                //      { 2, new DialogueSetting(npcNames[name], "말도 안 돼!! 그건 식문화의 붕괴라구!!!!", 0.02f) }
            };
        }
        else
        {

        }



    }
    private void OnEnable()
    {
        Idle();
    }

    public void Idle()
    {
        signal.SetActive(true);
    }

    public void Talk()
    {
        signal.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Idle();
    }
}

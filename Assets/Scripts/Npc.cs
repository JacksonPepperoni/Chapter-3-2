using System.Collections.Generic;
using Unity.VisualScripting;
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
    { Name.plant, "Ãæ½ÄÀÌ" },
    { Name.slug, "¿¡½ºÄ«¸£°í" },
     };


    [HideInInspector] public Dictionary<int, DialogueSetting> talkData;
    [SerializeField] GameObject signal;

    void Awake()
    {
        if (name == Name.plant)
        {
            talkData = new Dictionary<int, DialogueSetting>
            {
         //      { 0, new DialogueSetting(npcNames[name], "³Ê´Â ÆÄ¸® ¸ÔÀ» ¶§ ¹» »Ñ·Á¼­ ¸Ô´Ï?", 0.02f) },
         //      { 1, new DialogueSetting(npcNames[name], "¹¹? ÆÄ¸®¸¦ ¾È ¸Ô´Â´Ù°í?!", 0.02f) },
         //      { 2, new DialogueSetting(npcNames[name], "¸»µµ ¾È µÅ!! ±×°Ç ½Ä¹®È­ÀÇ ºØ±«¶ó±¸!!!!", 0.02f) }
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

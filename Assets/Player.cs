using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Progress;

public class Player : MonoBehaviour
{

    public enum State
    {
        Play,
        Stop,
        Event,
    }
    [HideInInspector] public State state = State.Play;


    private float speed = 7;


    [HideInInspector] public string name;

    public Item_Equip[] inven = new Item_Equip[21];
    public Item_Equip[] wear = new Item_Equip[3]; // 장비창

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;
    private Camera _camera;
    [HideInInspector]
    public Animator anim;
    private int hashIsWalk = Animator.StringToHash("isWalk");
    private int hashIsDuck = Animator.StringToHash("isDuck");


    private Vector2 moveInput;

    private Npc npc; // 접촉중인거

    [SerializeField] NameTag NameTagObj;



    public int atk;
    public int def;
    public int maxHp;


    void StateReset() //기본스탯
    {
        atk = 10;
        def = 10;
        maxHp = 3;
    }

    private void Awake()
    {
        _camera = Camera.main;
        anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = anim.gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

        Array.Clear(inven, 0, inven.Length);
        Array.Clear(wear, 0, wear.Length);

    }

    private void Start()
    {
        NameChange("산토끼토끼"); // 기본이름

        for (int i = 0; i < inven.Length; i++)
        {
            inven[i] = Managers.Data.items_Equip[UnityEngine.Random.Range(0, Managers.Data.items_Equip.Keys.Count)];
        }

        Managers.Game.OnEquipChanged += StatUpdate;


        StatUpdate();

    }


    public bool Inven_Add(Item_Equip item)
    {
        for (int i = 0; i < inven.Length; i++)
        {
            if (inven[i] == null)
            {
                inven[i] = item;
                return true;
            }

        }

        Debug.Log("인벤토리 공간 부족");
        return false;
    }
    public bool Inven_Delete(int number)
    {
        if (inven[number] != null)
        {
            inven[number] = null;
            return true;
        }

        Debug.Log("빈공간입니다");
        return false;
    }

    public bool Equip(int id)
    {
        for (int i = 0; i < wear.Length; i++)
        {
            if (wear[i] == null)
            {
                wear[i] = Managers.Data.items_Equip[id];
                Managers.Game.OnEquipChanged();

                Debug.Log($"{Managers.Data.items_Equip[id].name} 장착완료");
                return true;
            }
        }

        return false;
    }
    public bool UnEquip(int id)
    {
        for (int i = 0; i < wear.Length; i++)
        {
            if (wear[i] != null && wear[i].id == Managers.Data.items_Equip[id].id)
            {
                wear[i] = null;
                Managers.Game.OnEquipChanged();

                Debug.Log($"{Managers.Data.items_Equip[id].name} 장착해제");
                return true;
            }
        }
        return false;
    }

    void StatUpdate()
    {

        StateReset(); // 초기 스탯으로

        for (int i = 0; i < wear.Length; i++)
        {
            if (wear[i] != null)
            {
                atk += wear[i].buff_Atk;
                def += wear[i].buff_Def;
                maxHp += wear[i].buff_MaxHp;
            }
        }

    }


    private void Update()
    {
        if (anim.GetBool(hashIsDuck) || state != State.Play) return;


        rigid.MovePosition(rigid.position + moveInput);
        anim.SetBool(hashIsWalk, (moveInput.x != 0 || moveInput.y != 0));
        if (Mathf.Abs(moveInput.x) != 0) spriteRenderer.flipX = (moveInput.x < 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npc = collision.gameObject.GetComponent<Npc>();
            NameTagObj.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
        NameTagObj.gameObject.SetActive(true);
    }


    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■




    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().normalized * speed * Time.deltaTime;
    }

    public void OnDuck(InputValue value)
    {
        if (state != State.Play) return;

        anim.SetBool(hashIsDuck, value.isPressed);

    }
    public void OnJump(InputValue value)
    {
        if (state != State.Play) return;

        if (!value.isPressed) Debug.Log("점프");


    }
    public void OnTalk(InputValue value)
    {
        if (!value.isPressed) Debug.Log("대화");

        if (state == State.Stop) return;
        if (!value.isPressed || npc == null || anim.GetBool(hashIsDuck)) return;  // 눌렀을때 true 땔때 false 호출


        if (state == State.Event)
        {
            //     gameManager.dialogue.Talk();
            return;
        }

        state = State.Event;
        anim.SetBool(hashIsWalk, false);

        npc.Talk();
        //  gameManager.dialogue.talkDic = npc.talkData;
        //   gameManager.dialogue.Open();

    }


    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


    public void NameChange(string newName)
    {
        name = newName;
        NameTagObj.SizeChange(newName);
    }


}

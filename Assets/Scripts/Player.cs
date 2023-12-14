using UnityEngine;
using UnityEngine.InputSystem;

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

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;

    private Animator _anim;
    private int _hashIsWalk = Animator.StringToHash("isWalk");
    private int _hashIsDuck = Animator.StringToHash("isDuck");


    private Vector2 moveInput;

    private Npc npc; // 접촉중인거

    [SerializeField] private NameTag _nameTag;


    public bool[] isWearArray = new bool[21]; // 입고있는지
    public bool[] invenGetArray = new bool[21]; // 인벤 아이템 해금여부


    private void Awake()
    {

        _anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = _anim.gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        NameChange("산토끼토끼"); // 기본이름
        Managers.Game.OnEquipChanged -= StatUpdate;
        Managers.Game.OnEquipChanged += StatUpdate;

        StatUpdate();

    }

    private void OnDisable()
    {
        Managers.Game.OnEquipChanged -= StatUpdate;
    }


    public void Inven_Add(int number)
    {
        Managers.Data.userData.invenGetArray[number] = true;
        Managers.Data.SaveUserDataToJson();

    }
    public void Inven_Delete(int number)
    {
        Managers.Data.userData.invenGetArray[number] = false;
        Managers.Data.SaveUserDataToJson();
    }

    void StatUpdate()
    {
        Managers.Data.userData.maxHp = Managers.Data.userData.de_maxHp;
        Managers.Data.userData.atk = Managers.Data.userData.de_atk;
        Managers.Data.userData.def = Managers.Data.userData.de_def;


        for (int i = 0; i < Managers.Data.userData.isWearArray.Length; i++)
        {
            if (Managers.Data.userData.isWearArray[i])
            {
                Managers.Data.userData.atk += Managers.Data.items_Equip[i].buff_Atk;
                Managers.Data.userData.def += Managers.Data.items_Equip[i].buff_Def;
                Managers.Data.userData.maxHp += Managers.Data.items_Equip[i].buff_MaxHp;

            }
        }
        Managers.Data.SaveUserDataToJson();
    }


    private void Update()
    {
        if (_anim.GetBool(_hashIsDuck) || state != State.Play) return;


        rigid.MovePosition(rigid.position + moveInput);
        _anim.SetBool(_hashIsWalk, (moveInput.x != 0 || moveInput.y != 0));
        if (Mathf.Abs(moveInput.x) != 0) spriteRenderer.flipX = (moveInput.x < 0);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            npc = collision.gameObject.GetComponent<Npc>();
            _nameTag.gameObject.SetActive(false);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        npc = null;
        _nameTag.gameObject.SetActive(true);
    }


    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■




    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().normalized * speed * Time.deltaTime;
    }

    public void OnDuck(InputValue value)
    {
        if (state != State.Play) return;

        _anim.SetBool(_hashIsDuck, value.isPressed);

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
        if (!value.isPressed || npc == null || _anim.GetBool(_hashIsDuck)) return;  // 눌렀을때 true 땔때 false 호출


        if (state == State.Event)
        {
            //     gameManager.dialogue.Talk();
            return;
        }

        state = State.Event;
        _anim.SetBool(_hashIsWalk, false);

        npc.Talk();
        //  gameManager.dialogue.talkDic = npc.talkData;
        //   gameManager.dialogue.Open();

    }


    //■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■■


    public void NameChange(string newName)
    {
        name = newName;
        _nameTag.SizeChange(newName);
    }


}

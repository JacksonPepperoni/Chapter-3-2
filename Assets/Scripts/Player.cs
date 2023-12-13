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
    private Camera _camera;
    [HideInInspector]
    public Animator anim;
    private int hashIsWalk = Animator.StringToHash("isWalk");
    private int hashIsDuck = Animator.StringToHash("isDuck");


    private Vector2 moveInput;

    private Npc npc; // ¡¢√À¡ﬂ¿Œ∞≈

    [SerializeField] NameTag NameTagObj;



    public int atk = 0;
    public int def = 0;
    public int maxHp = 0;




    private void Awake()
    {
        _camera = Camera.main;
        anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = anim.gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        NameChange("ªÍ≈‰≥¢≈‰≥¢"); // ±‚∫ª¿Ã∏ß
        Managers.Game.OnEquipChanged += StatUpdate;

        StatUpdate();

    }


    public bool Inven_Add(int number)
    {
        Managers.Data.userData.invenGetArray[number] = true;

        Managers.Data.SaveUserDataToJson();
        return true;

    }
    public bool Inven_Delete(int number)
    {
        Managers.Data.userData.invenGetArray[number] = false;

        Managers.Data.SaveUserDataToJson();
        return true;
    }
    void StateReset() //±‚∫ªΩ∫≈»
    {
        atk = 10;
        def = 10;
        maxHp = 3;
    }
    void StatUpdate()
    {
        StateReset(); // √ ±‚ Ω∫≈»¿∏∑Œ

        for (int i = 0; i < Managers.Data.userData.isWearArray.Length; i++)
        {
            if (Managers.Data.userData.isWearArray[i])
            {
                atk += Managers.Data.items_Equip[i].buff_Atk;
                def += Managers.Data.items_Equip[i].buff_Def;
                maxHp += Managers.Data.items_Equip[i].buff_MaxHp;
            }
        }

       Managers.Game.OnStateTextChanged();

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


    //°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·




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

        if (!value.isPressed) Debug.Log("¡°«¡");


    }
    public void OnTalk(InputValue value)
    {
        if (!value.isPressed) Debug.Log("¥Î»≠");

        if (state == State.Stop) return;
        if (!value.isPressed || npc == null || anim.GetBool(hashIsDuck)) return;  // ¥≠∑∂¿ª∂ß true ∂™∂ß false »£√‚


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


    //°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·°·


    public void NameChange(string newName)
    {
        name = newName;
        NameTagObj.SizeChange(newName);
    }


}

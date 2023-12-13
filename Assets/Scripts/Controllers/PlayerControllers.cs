using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllers : MonoBehaviour
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


    private Camera _camera;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigid;


    [HideInInspector]
    public Animator anim;
    private int hashIsWalk = Animator.StringToHash("isWalk");
    private int hashIsDuck = Animator.StringToHash("isDuck");


    private Vector2 moveInput;
    private Vector2 mousePos;

    private Npc npc; // �������ΰ�

    [SerializeField] NameTag NameTagObj;



    private void Awake()
    {
        _camera = Camera.main;
        anim = transform.GetChild(0).GetComponent<Animator>();
        spriteRenderer = anim.gameObject.GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        NameChange("���䳢�䳢"); // �⺻�̸�
    }


    private void Update()
    {
        if (anim.GetBool(hashIsDuck) || state != State.Play) return;


        rigid.MovePosition(rigid.position + moveInput); // �̵�. ���ν�Ƽ ����ϴ°žƴѰ� �浹ó���Ϸ���?
        anim.SetBool(hashIsWalk, (moveInput.x != 0 || moveInput.y != 0)); // ���ֱ� �ٱ� �ִ� ��ȯ
        if (Mathf.Abs(moveInput.x) != 0) spriteRenderer.flipX = (moveInput.x < 0); // �����ִ� ���� ����

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


    //�����������������������������������������������������������������������������������������������




    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>().normalized * speed * Time.deltaTime;
    }

    public void OnLook(InputValue value) // ���콺 �������� �ٲ𶧸� �̶� ���콺 ���߰� ĳ���Ͱ� ���콺 ������ �̵������� �¿������ �ߵ� ����.... ������������ ���콺 ��ġ��
    {

        if (anim.GetBool(hashIsDuck) || state != State.Play) return;

        mousePos = _camera.ScreenToWorldPoint(value.Get<Vector2>());
        spriteRenderer.flipX = (rigid.position.x > mousePos.x);
    }

    public void OnDuck(InputValue value)
    {
        if (state != State.Play) return;

        anim.SetBool(hashIsDuck, value.isPressed);

    }

    public void OnTalk(InputValue value)
    {
        if (state == State.Stop) return;
        if (!value.isPressed || npc == null || anim.GetBool(hashIsDuck)) return;  // �������� true ���� false ȣ��


        if (state == State.Event)
        {
         //   gameManager.dialogue.Talk();
            return;
        }

        state = State.Event;
        anim.SetBool(hashIsWalk, false);

        npc.Talk();
      //  gameManager.dialogue.talkDic = npc.talkData;
      ///  gameManager.dialogue.Open();

    }


    //�����������������������������������������������������������������������������������������������


    public void NameChange(string newName)
    {
        name = newName;
        NameTagObj.SizeChange(newName);
    }


}

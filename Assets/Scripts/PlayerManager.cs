using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;
using UnityEngine.UI;
using DG.Tweening;

public enum PlayerState {Seeking, hasShell, Died, Finished}

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;


    public bool startGetHurted;

    public PlayerState m_playerState;

    private Rigidbody2D m_rigid;
    private Vector2 m_velocity;

    [SerializeField]
    private FloatVariable moveSpeed;

    [SerializeField]
    private float init_moveSpeed;

    private GameObject The_Shell_Encountered;
    public GameObject cur_home;

    [SerializeField]
    private float MaxHP, HP_Loss_perSecond;

    private float curHP, curShield;


    public Image Image_curHP, Image_maxHP, Image_curShield;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        startGetHurted = false;

        curHP = MaxHP;

        m_playerState = PlayerState.Seeking;
        m_rigid = GetComponent<Rigidbody2D>();
        moveSpeed.Value = init_moveSpeed;


        Image_curHP.enabled = false;
        Image_maxHP.enabled = false;
        Image_curShield.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        m_velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        Image_curHP.fillAmount = curHP / MaxHP;
        Image_curShield.fillAmount = curShield / MaxHP;


        switch (m_playerState)
        {
            case PlayerState.Seeking:
                if (The_Shell_Encountered && Input.GetButtonDown("Interact"))
                {
                    Become_Home(The_Shell_Encountered);
                }

                if (curHP <= 0)
                {
                    Died();
                }

                break;
            case PlayerState.hasShell:
                cur_home.transform.localPosition = Vector3.zero;

                if (Input.GetButtonDown("Interact"))
                {
                    Leave_Home();
                }

                if (cur_home!=null && curShield <= 0)
                {
                    Leave_Home();
                }
                break;
            case PlayerState.Died:
                break;
        }



    }

    private void FixedUpdate()
    {
        m_rigid.velocity = m_velocity * moveSpeed.Value;

        switch (m_playerState)
        {
            case PlayerState.Seeking:
                if(startGetHurted)
                    curHP -= HP_Loss_perSecond * Time.fixedDeltaTime;



                break;
            case PlayerState.hasShell:

                //護盾的損失速度比一般生命值慢2倍
                if(startGetHurted)
                    curShield -= HP_Loss_perSecond * Time.fixedDeltaTime;

                break;
            case PlayerState.Died:
                break;
        }
    
    }

    public void TurnOff_SafeMode()
    {
        startGetHurted = true;
        Image_curHP.enabled = true;
        Image_maxHP.enabled = true;
        Image_curShield.enabled = true;
             

    }

    public void Leave_Home()
    {
        cur_home.GetComponent<Shell>().Get_Dumped();
        cur_home = null;
        curShield = 0;

        moveSpeed.Value = init_moveSpeed;
        m_playerState = PlayerState.Seeking;
    }

    public void Become_Home(GameObject newHome)
    {
        cur_home = newHome;



        newHome.transform.SetParent(this.gameObject.transform);

        Color32 initColor = newHome.GetComponent<SpriteRenderer>().color;
        newHome.GetComponent<SpriteRenderer>().DOColor(new Color32(initColor.r, initColor.g, initColor.b, 100), 0.3f);

        cur_home.transform.DOLocalMove(Vector3.zero, 0.3f, false).SetEase(Ease.Linear).OnComplete(() => m_playerState = PlayerState.hasShell);

        moveSpeed.Value = init_moveSpeed + cur_home.GetComponent<Shell>().speedDebuff;

        curShield = cur_home.GetComponent<Shell>().shieldProvided;

        
    }

    public void Died()
    {
        m_playerState = PlayerState.Died;
        SceneStateManager.instance.Call_Replay();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Shell")
        {
            The_Shell_Encountered = collision.gameObject;
        }
        else if(collision.gameObject.tag == "NarrativeTrigger")
        {
            NarrativeManager.instance.cur_narrative.Value = collision.gameObject.GetComponent<Narrative_Trigger>().the_sentence;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == The_Shell_Encountered)
            The_Shell_Encountered = null;
    }



}

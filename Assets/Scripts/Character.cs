using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
public class Character : Clickable
{
    public int id = 0;


    private GameObject highlight;

    private GameObject options;

    private bool selected = false;

    private NavMeshAgent navAgent;

    [SerializeField]
    private UIClickable closeButton;

    [SerializeField]
    private UIClickable moveButton;

    [SerializeField]
    private UIClickable attackButton;

    private CharacterGridMovement moveScript;

    private CharacterAttack attackScript;

    [SerializeField]
    public int total_health;

    public int current_health;

    
    public int team = 0;

    private PlayerManager myManager;

    public bool friendly = true;

    public string characterType;

    public enum characterState { move, attack, idle };

    public characterState myState;

    public AnimationController animator;

    bool isMoving = false;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        attackScript = this.gameObject.GetComponent<CharacterAttack>();
        moveScript = this.gameObject.GetComponent<CharacterGridMovement>();
        animator = GetComponentInChildren<AnimationController>();
        myState = characterState.idle;
        //myManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        highlight = transform.Find("Highlight").gameObject;//might be better to SerializeField these and drag n drop
        
        if (friendly)
        {
            options = transform.Find("Canvas").gameObject;
            options.SetActive(false);
            closeButton.clickHandler += DeselectClicked;
            moveButton.clickHandler += MoveClicked;
            attackButton.clickHandler += AttackClicked;
        }
        highlight.SetActive(false);
        
        
    }

    private void Awake()
    {
        current_health = total_health;
    }


    public void SetPlayerManager(PlayerManager p)
    {
        myManager = p;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        /*if (navAgent.remainingDistance==0&&isMoving)
        {
            Debug.Log("STOP");
            animator.AnimateIdle();
            isMoving = false;
        }*/
        
    }

    public override void Highlighted()
    {
        base.Highlighted();
        
        highlight.SetActive(true);
    }
    public override void UnHighlighted()
    {
        base.UnHighlighted();
        if(!selected)
            highlight.SetActive(false);
    }

    protected override void OnClicked()
    {
        //Debug.Log("Character clicked on!");
        if (friendly)//is mine
            Select(false);
        else
        {
            if (GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().isServer)
                myManager.AttackTarget(id);
            else
            {
                //GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().myFunc();
                GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().CmdAttackTarget(id);
                Debug.Log("Client Clicked");
            }
            
        }
    }

    public void Select(bool fromMenu)
    {
        if (!fromMenu)
        {
            if (GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().isServer)
                myManager.SetSelectedCharacter(this);
            else
            {
                //GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().myFunc();
                GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().CmdSetSelectedCharacter(id);
                Debug.Log("Client Clicked");
            }
        }
        selected = true;
        highlight.SetActive(true);
        options.SetActive(true);
    }

    public void DeselectClicked()
    {
        Deselect(false);
    }

    public void Deselect(bool fromManager)
    {
        if (!fromManager)
        {
            if (GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().isServer)
                myManager.SetSelectedCharacter(null);
            else
            {
                //GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().myFunc();
                GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().CmdSetSelectedCharacter(-1);
               
            }
            //myManager.SetSelectedCharacter(null);
        }
        selected = false;
        options.SetActive(false);
        UnHighlighted();

    }

    public void MoveTo(Vector3 where)
    {
        /*isMoving = true;
        navAgent.SetDestination(where);
        animator.AnimateMove();*/
        Deselect(false);
        myState = characterState.idle;
    }

    public void StartMoving()
    {
        Debug.Log("MOVING!");
        Deselect(false);
        myState = characterState.idle;
    }

    private void MoveClicked()
    {
        moveScript.Highlight_Reachable();
        myState = characterState.move;//maybe later on this can include a visual cursor showing up on the terrain indicating validity of move
        options.SetActive(false);
    }

    public void ToggleMoveButton()
    {
        moveButton.enabled = !moveButton.enabled;
    }

    private void AttackClicked()
    {
        attackScript.DisplayAttackRange();
        myState = characterState.attack;
        options.SetActive(false);
    }

    private IEnumerator coroutine;

    public void AttackEnemy(Character enemy)
    {
        attackScript.Attack(enemy);
        coroutine = WaitToIdle(enemy);
        StartCoroutine(coroutine);
    }

    IEnumerator WaitToIdle(Character enemy)
    {
        yield return new WaitForSeconds(5);
        myState = characterState.idle;
        if (enemy.current_health > 0)
        {
            enemy.animator.AnimateIdle();
            enemy.myState = characterState.idle;
        } else
        {
            Destroy(enemy.gameObject);
        }
    }

    public void ToggleAttackButton()
    {
        attackButton.enabled = !attackButton.enabled;
    }

    public void SetFriendly(bool afriendly)
    {
        if (!afriendly)
        {
            friendly = false;
            highlight.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0, .5f);
          
        }
    }

}

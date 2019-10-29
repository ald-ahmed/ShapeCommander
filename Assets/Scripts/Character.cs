using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : Clickable
{


    public GameObject attackOrb;

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

    private PlayerManager myManager;

    public bool friendly = true;

    public enum characterState { move, attack, idle };

    public characterState myState;


    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        myState = characterState.idle;
        myManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
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
        navAgent = gameObject.GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
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
        if (friendly)
            Select(false);
        else
            myManager.SetTargetedEnemy(this);
    }

    public void Select(bool fromMenu)
    {
        if (!fromMenu)
        {
            myManager.SetSelectedCharacter(this);
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
            myManager.SetSelectedCharacter(null);
        selected = false;
        options.SetActive(false);
        UnHighlighted();

    }

    public void MoveTo(Vector3 where)
    {
        navAgent.SetDestination(where);
        Deselect(false);
        myState = characterState.idle;
    }

    private void MoveClicked()
    {
        myState = characterState.move;//maybe later on this can include a visual cursor showing up on the terrain indicating validity of move
        options.SetActive(false);
    }

    private void AttackClicked()
    {
        myState = characterState.attack;
        options.SetActive(false);
    }

    public void AttackEnemy(Character enemy)
    {
        //TODO: Ahmed + Uche
        //can get the enemy's position: enemy.transform.position.  Our position = transform.position

        float step = (float)(0.5 * Time.deltaTime);
        attackOrb.transform.position = Vector3.MoveTowards(transform.position, enemy.transform.position, step);

        if (attackOrb.transform.position == enemy.transform.position) {
            attackOrb.GetComponent<Renderer>().enabled = false;
            attackOrb.GetComponent<ParticleSystem>().enableEmission = false;
        }
        else
        {

            attackOrb.GetComponent<Renderer>().enabled = true;
            attackOrb.GetComponent<ParticleSystem>().enableEmission = true;

        }


        Debug.Log("Attacking enemy!");
        Deselect(false);
        myState = characterState.idle;
    }


}

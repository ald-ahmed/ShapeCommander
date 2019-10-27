using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Character : Clickable
{
    private GameObject highlight;

    private GameObject options;

    private bool selected = false;

    private NavMeshAgent navAgent;

    [SerializeField]
    private UIClickable closeButton;

    [SerializeField]
    private UIClickable moveButton;

    private PlayerManager myManager;
    
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        myManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        highlight = transform.Find("Highlight").gameObject;//might be better to SerializeField these and drag n drop
        options = transform.Find("Canvas").gameObject;
        options.SetActive(false);
        highlight.SetActive(false);
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        closeButton.clickHandler += DeselectClicked;
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
        Select(false);
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
    }

}

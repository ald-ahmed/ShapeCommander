using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Clickable
{
    private GameObject highlight;
    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        highlight = transform.Find("Highlight").gameObject;
        highlight.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public override void Highlighted()
    {
        base.Highlighted();
        Debug.Log("HELLLLLLLO");
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
        Debug.Log("Character clicked on!");
        selected = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHex : Clickable
{
    private Tile m_myTile;
    protected override void OnClicked()
    {
        
    }

    public override void Highlighted()
    {
        base.Highlighted();
        //maybe only if the tile is in range
        m_myTile.Highlight();
    }

    public override void UnHighlighted()
    {
        base.UnHighlighted();
        m_myTile.UnHighlight();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_myTile = GetComponent<Tile>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHex : Clickable
{
    private Tile m_myTile;
    private PlayerManager myManager;

    protected override void OnClicked()
    {
        Debug.Log("CLICKED");
        if (myManager.GetSelectedCharacter() == null)
        {
            Debug.Log("Selected character is null");
        }
        else if (myManager.GetSelectedCharacter().myState == Character.characterState.move)
        {
            Debug.Log("selected character should move");
            //myManager.GetSelectedCharacter().GetComponent<CharacterGridMovement>().AddToPath(m_myTile);
            //myManager.GetSelectedCharacter().GetComponent<CharacterGridMovement>().Navigate();
            //myManager.GetSelectedCharacter().StartMoving();
        }
    }

    public override void Highlighted()
    {
        base.Highlighted();
        //maybe only if the tile is in range
        m_myTile.Highlight();
        //Debug.Log("Highlight");
    }

    public override void UnHighlighted()
    {
        base.UnHighlighted();
        Debug.Log("Unhighlight");
        m_myTile.UnHighlight();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_myTile = GetComponent<Tile>();
        myManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}

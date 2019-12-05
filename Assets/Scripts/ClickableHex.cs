using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableHex : Clickable
{
    private Tile m_myTile;
    private PlayerManager myManager;

    protected override void OnClicked()
    {
        if (myManager.GetSelectedCharacter() == null)
        {
            Debug.Log("Selected character is null");
        }
        else if (myManager.GetSelectedCharacter().myState == Character.characterState.move)
        {
            //Debug.Log("selected character should move");//going to move this stuff to the PlayerManager for networking

            if (GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().isServer)
                myManager.SendSelectedCharacter(m_myTile.index);
            else
                GameObject.Find("LocalPlayer").GetComponent<PlayerFace>().CmdSendSelected(m_myTile.index);


            
            /*myManager.GetSelectedCharacter().GetComponent<CharacterGridMovement>().AddToPath(m_myTile);
            myManager.GetSelectedCharacter().GetComponent<CharacterGridMovement>().Navigate();
            myManager.GetSelectedCharacter().StartMoving();*/

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
        m_myTile.UnHighlight();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        m_myTile = GetComponent<Tile>();
        //myManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }

    public void SetPlayerManager(PlayerManager p)
    {
        myManager = p;
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }
}

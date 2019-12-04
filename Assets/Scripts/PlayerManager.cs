using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerManager : NetworkBehaviour
{

    //temporary bs
    public List<Character> characters = new List<Character>();

    public Character selectedCharacter=null;

    private Character targetedEnemy = null;

    [SerializeField]
    private Grid grid;

    private void Awake()
    {
        foreach (Character c in characters)
        {
            c.SetPlayerManager(this);
        }

        ClickableHex[] tiles = GameObject.FindObjectsOfType<ClickableHex>();
        foreach (ClickableHex c in tiles)
        {
            c.SetPlayerManager(this);
        }
    }
    private void Start()
    {
        
    }

    public void TestFunction()
    {
        Debug.Log("Test function");
        RpcTestCall();
    }

    public void KillPlayer(int id)
    {
        Character toRemove=null;
        foreach (Character c in characters)
        {
            if (c)
            {
                if (c.id == id)
                    toRemove = c;
            }
        }
        if(toRemove)
            characters.Remove(toRemove);

        bool team1 = false;
        bool team0 = false;
        foreach(Character c in characters)
        {
            if (c)
            {
                if (c.team == 0)
                    team0 = true;
                else
                    team1 = true;
            }
        }

        if (!team1)
        {
            DeclareVictory(0);
        }
        if (!team0)
        {
            DeclareVictory(1);
        }
    }

    private void DeclareVictory(int team)
    {
        PlayerFace[] players = GameObject.FindObjectsOfType<PlayerFace>();
        foreach(PlayerFace p in players)
        {
            p.WinnerDeclared(team);
        }
    }

    [ClientRpc]
    public void RpcTestCall()
    {
       
            Debug.Log("HELLOOO");
    }

    //BE VERY CAREFUL WITH THIS SO YOU DONT GET LOOPS
    public void SetSelectedCharacter(Character c)
    {
        /* if (selectedCharacter != null)
         {
             selectedCharacter.Deselect(true);
         }
         selectedCharacter = c;*/
        // if (selectedCharacter != null)
        //{
        //selectedCharacter.Select(true);
        //}

        //determine identifier of character and pass that in (can't pass in component)
        Debug.Log("Setting selected character");
        int id;
        if (c != null)
            id = c.id;
        else
            id = -1;
       
            Debug.Log("A");
            RpcSetSelected(id);
        
        
    }
    public void FlipTurns()
    {
        RpcFlipTurns();
    }

    public void AttackTarget(int target)
    {
        RpcAttackTarget(target);
    }

    public void SetTargetedEnemy(Character c)
    {
        if (selectedCharacter != null)
        {
            if (selectedCharacter.myState == Character.characterState.attack)
            {
                targetedEnemy = c;
                selectedCharacter.AttackEnemy(targetedEnemy);
            }
            
        }
        
    }

    public Character GetSelectedCharacter()
    {
        return selectedCharacter;
    }


    /*public void SendSelectedCharacter(Vector3 where) old
    {
        if(selectedCharacter!=null&&selectedCharacter.myState==Character.characterState.move)
            selectedCharacter.MoveTo(where);
    }*/
    public void SendSelectedCharacter(CubeIndex target)
    {
     
        
            RpcSendSelected(target);
        
    }

    public Character GetCharacterByID(int i)
    {
        foreach(Character c in characters)
        {
            if (c)
            {
                if (c.id == i)
                    return c;
            }
        }
        return null;
    }

    [ClientRpc]
    public void RpcFlipTurns()
    {
        PlayerFace[] pfs = GameObject.FindObjectsOfType<PlayerFace>();

        foreach(PlayerFace p in pfs)
        {
            p.FlipTurns();
        }
    }

    [ClientRpc]
    public void RpcAttackTarget(int target)
    {
        if(GetCharacterByID(target)!=null)
            selectedCharacter.AttackEnemy(GetCharacterByID(target));
    }

    [ClientRpc]
    public void RpcSendSelected(CubeIndex target)
    {
        Debug.Log("RpcSendSelected");
        // selectedCharacter.GetComponent<CharacterGridMovement>().AddToPath(grid.TileAt(target));
        //selectedCharacter.GetComponent<CharacterGridMovement>().Navigate();
        //selectedCharacter.StartMoving();
        selectedCharacter.GetComponent<CharacterGridMovement>().MoveToDestination(grid.TileAt(target));
        selectedCharacter.StartMoving();
    }
    [Command]
    public void CmdSendSelected(CubeIndex target)
    {
        Debug.Log("CmdSendSelected");
        /* selectedCharacter.GetComponent<CharacterGridMovement>().AddToPath(grid.TileAt(target));
         selectedCharacter.GetComponent<CharacterGridMovement>().Navigate();
         selectedCharacter.StartMoving();*/
        RpcSendSelected(target);
    }

    [ClientRpc]
    public void RpcSetSelected(int id)
    {
        //convert primitive identifier back into a Character and select it
        Debug.Log("in RPC");
        if (selectedCharacter != null)
        {
            selectedCharacter.Deselect(true);
        }
        selectedCharacter = GetCharacterByID(id);
    }
    [Command]
    public void CmdSetSelected(int id)
    {
        //convert primitive identifier back into a Character and select it
        Debug.Log("in CMD");
        /* if (selectedCharacter != null)
         {
             selectedCharacter.Deselect(true);
         }
         selectedCharacter = GetCharacterByID(id);*/
        RpcSetSelected(id);
    }
}

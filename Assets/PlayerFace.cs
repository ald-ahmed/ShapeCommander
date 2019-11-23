using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.XR.MagicLeap;
public class PlayerFace : NetworkBehaviour
{
    //private Transform m_target;
    private MyControl m_placementControl;
    private PlayerManager m_playerManager;

    private UIClickable m_endTurnButton;
    private GameObject m_beam;
    private bool m_isMyTurn = true;
    Character[] units;
    int myTeam;
    //your local player needs to have a recognizable name that allows it to be found for assigning network authority
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.name = "LocalPlayer";
        //m_target = GameObject.Find("Main Camera").transform;

        m_endTurnButton = GameObject.Find("EndTurnButton").GetComponent<UIClickable>();
        m_beam = GameObject.Find("Beam");
        m_endTurnButton.clickHandler += ClickedEndTurn;



        if (GetComponent<NetworkIdentity>().isServer)
        {
            myTeam = 1;

        }
        else
        {
            m_isMyTurn = false;
            
            myTeam = 0;
            m_beam.SetActive(false);
            m_endTurnButton.gameObject.SetActive(false);
        }
        if (GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer p in m)
            {
                p.enabled = false;
            }
        }

        units = GameObject.FindObjectsOfType<Character>();
        foreach (Character c in units)
        {
            if (c.team != myTeam)
            {
                c.SetFriendly(false);
            }
        }
        
        //m_placementControl = GetComponent<MyControl>();
        //m_playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
       // m_placementControl.allowPlacement = true;
        //CmdTestCommand();
    }

    void Start()
    {
        

    }

    void Update()
    {
       
    }

    public void FlipTurns()
    {
        if (isLocalPlayer)
        {
            m_isMyTurn = !m_isMyTurn;
            Debug.Log("Is it my turn now: " + m_isMyTurn);

            if (m_isMyTurn)
            {
                m_beam.SetActive(true);
                foreach (Character c in units)
                {
                    if (c.team == myTeam)
                    {
                        c.GetComponent<CharacterGridMovement>().ResetRemainingMoves();
                        c.EnableButtons();
                        m_endTurnButton.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                m_beam.SetActive(false);
                m_endTurnButton.gameObject.SetActive(false);
            }
        }
    }

    public void ClickedEndTurn()
    {
        if (GetComponent<NetworkIdentity>().isServer)
        {
            GameObject.Find("PlayerManager").GetComponent<PlayerManager>().FlipTurns();

        }
        else
            CmdFlipTurns();
    }

    [Command]
    public void CmdFlipTurns()
    {

        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().FlipTurns();
    }
    

    [Command]
    public void CmdTestCommand()
    {
        Debug.Log("Test Command");
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().TestFunction();
    }

    [Command]
    public void CmdSetSelectedCharacter(int id)
    {
        Debug.Log("Cmd set selected character: " + id);
        //GameObject.Find("PlayerManager").GetComponent<PlayerManager>().TestFunction();
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().SetSelectedCharacter(GameObject.Find("PlayerManager").GetComponent<PlayerManager>().GetCharacterByID(id));

    }

    [Command]
    public void CmdAttackTarget(int id)
    {
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().AttackTarget(id);
    }

    [Command]
    public void CmdSendSelected(CubeIndex target)
    {
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().SendSelectedCharacter(target);
    }

}

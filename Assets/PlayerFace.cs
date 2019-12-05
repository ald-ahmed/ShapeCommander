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

    private BannerManager m_bannerManager;

    private GameObject m_beam;
    private bool m_isMyTurn = true;
    Character[] units;
    int myTeam;
    private float lastPing = 0.0f;
    //your local player needs to have a recognizable name that allows it to be found for assigning network authority
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.name = "LocalPlayer";
        //m_target = GameObject.Find("Main Camera").transform;
        m_bannerManager = GameObject.Find("Turn Banner").GetComponent<BannerManager>();
        m_endTurnButton = GameObject.Find("EndTurnButton").GetComponent<UIClickable>();
        m_beam = GameObject.Find("Beam");
        m_endTurnButton.clickHandler += ClickedEndTurn;



        if (GetComponent<NetworkIdentity>().isServer)
        {
            myTeam = 1;
            m_bannerManager.ShowYourTurn();
        }
        else
        {
            m_isMyTurn = false;
            
            myTeam = 0;
            m_beam.transform.GetComponentInChildren<Collides>().enabled = false;
            m_beam.transform.GetComponentInChildren<LineRenderer>().enabled = false;
            //m_beam.SetActive(false);
            m_bannerManager.ShowOpponentsTurn();
            //m_endTurnButton.gameObject.SetActive(false);
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
        ConnectionConfig myConfig = new ConnectionConfig();
        myConfig.DisconnectTimeout = 300000;
        myConfig.PingTimeout = 100000;
    }

    void Update()
    {
        if (!isServer && lastPing + Time.deltaTime > 5.0)
        {
            lastPing = Time.deltaTime;
            CmdKeepAlive();
            Debug.Log("Sent a keepalive");
        }
    }

    public void WinnerDeclared(int winner)
    {
        if (isLocalPlayer)
        {
            if (winner == myTeam)
            {
                m_bannerManager.ShowWinMessage();
                m_beam.SetActive(false);

            }
            else
            {
                m_bannerManager.ShowLostMessage();
                m_beam.SetActive(false);
            }
        }
    }

    public void FlipTurns()
    {
        if (isLocalPlayer)
        {
            m_isMyTurn = !m_isMyTurn;
            Debug.Log("Is it my turn now: " + m_isMyTurn);

            if (m_isMyTurn)
            {
                //m_beam.SetActive(true);
                m_beam.transform.GetComponentInChildren<LineRenderer>().enabled = true;
                m_beam.transform.GetComponentInChildren<Collides>().enabled = true;
                
                foreach (Character c in units)
                {
                    if (c)//null check
                    {
                        c.GetComponent<CharacterGridMovement>().ResetRemainingMoves();
                        if (c.team == myTeam)
                        {
                            
                            c.EnableButtons();
                            m_bannerManager.ShowYourTurn();
                            // m_endTurnButton.gameObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                //m_beam.SetActive(false);
                m_beam.transform.GetComponentInChildren<Collides>().enabled = false;
                m_beam.transform.GetComponentInChildren<LineRenderer>().enabled = false;
                m_bannerManager.ShowOpponentsTurn();
               // m_endTurnButton.gameObject.SetActive(false);
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

    [Command]
    public void CmdKeepAlive()
    {
        Debug.Log("keepalive from client");
    }

}

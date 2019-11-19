using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerFace : NetworkBehaviour
{
    private Transform m_target;
    private bool m_canPlaceMap = false;
    private MyControl m_placementControl;
    private PlayerManager m_playerManager;

    //your local player needs to have a recognizable name that allows it to be found for assigning network authority
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        gameObject.name = "LocalPlayer";
        //CmdTestCommand();
    }

    void Start()
    {
        m_target = GameObject.Find("Main Camera").transform;
        m_placementControl = GetComponent<MyControl>();
        m_playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();

        if (GetComponent<NetworkIdentity>().isServer)
        {
            m_canPlaceMap = true;
            Debug.Log("I can place the map");
            m_placementControl.allowPlacement = true;
        }
        if (GetComponent<NetworkIdentity>().isLocalPlayer)
        {
            MeshRenderer[] m = GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer p in m)
            {
                p.enabled = false;
            }
        }
    }

    void Update()
    {
        if (m_target != null)
        {
            transform.position = m_target.position;
            transform.rotation = m_target.rotation;
        }
    }
    public void myFunc()
    {
        Debug.Log("MyFunc");
        CmdSetSelectedCharacter(0);
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
    public void CmdSendSelected(CubeIndex target)
    {
        GameObject.Find("PlayerManager").GetComponent<PlayerManager>().SendSelectedCharacter(target);
    }

}

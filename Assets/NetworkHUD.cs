using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class NetworkHUD : MonoBehaviour
{

    [SerializeField]
    private UIClickable m_joinButton;

    [SerializeField]
    private UIClickable m_hostButton;



    private NetworkManager m_networkManager;




    void Start()
    {
        m_networkManager = gameObject.GetComponent<NetworkManager>();
        m_joinButton.clickHandler += StartConnecting;
        m_hostButton.clickHandler += StartHosting;

    }

 

    public void StartHosting()
    {
        //m_networkManager.networkAddress= "10.26.101.236";
        m_networkManager.StartHost();
        Debug.Log("Starting hosting on "+m_networkManager.networkAddress);
        GameObject.Find("NetworkingPanel").SetActive(false);
    }

    public void StartConnecting()
    {
        
        m_networkManager.StartClient();
        Debug.Log("Starting connecting to "+m_networkManager.networkAddress);
        GameObject.Find("NetworkingPanel").SetActive(false);
    }


}

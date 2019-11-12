using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerFace : MonoBehaviour
{
    private Transform m_target;
    private bool m_canPlaceMap = false;

    void Start()
    {
        m_target = GameObject.Find("Main Camera").transform;
        if (GetComponent<NetworkIdentity>().isServer)
        {
            m_canPlaceMap = true;
            Debug.Log("I can place the map");
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

}

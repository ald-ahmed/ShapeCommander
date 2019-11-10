using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFace : MonoBehaviour
{
    private Transform m_target;
    // Start is called before the first frame update
    void Start()
    {
        m_target = GameObject.Find("Main Camera").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target != null)
        {
            transform.position = m_target.position;
            transform.rotation = m_target.rotation;
        }
    }
}

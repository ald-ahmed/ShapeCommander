using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTest : MonoBehaviour
{
    [SerializeField]
    private UIClickable m_buttonOne;
    // Start is called before the first frame update
    void Start()
    {
        m_buttonOne.clickHandler += Reaction;
    }

    void Reaction()
    {
        Debug.Log("CLICKED");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

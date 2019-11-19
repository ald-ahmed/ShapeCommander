using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Required when Using UI elements.

public class ChangeHealth : MonoBehaviour
{


    public Image myImage;

    // Start is called before the first frame update
    void Start()
    {

        myImage = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void setHealth(float newHealth)
    {
        myImage.fillAmount = newHealth;
    }


}

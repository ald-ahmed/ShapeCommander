using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public abstract class Clickable : MonoBehaviour
{
    protected MLInputController _controller;
    protected bool pressed = false;
    protected bool highlighted = false;
   
    // Start is called before the first frame update
    protected void Start()
    {
        //Start receiving input by the Control
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        
    }
    void OnDestroy()
    {
        //Stop receiving input by the Control
        MLInput.Stop();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (_controller.TriggerValue > 0.2f && !pressed)
        {
            
            pressed = true;

            //if being touched by laser pointer at this point
            if(highlighted)
                OnClicked();
            
        }
        else
        {
            pressed = false;
        }
    }
  

    public virtual void Highlighted()
    {
        //Debug.Log("Highlighted");
        highlighted = true;
    }
    public virtual void UnHighlighted()
    {
        //Debug.Log("Unhighlighted");
        highlighted = false;
    }

    protected abstract void OnClicked();
}

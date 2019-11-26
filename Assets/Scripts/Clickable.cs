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
            //Debug.Log("Trigger registered");
            pressed = true;

            //if being touched by laser pointer at this point
            if (highlighted)
            {
                //Debug.Log("highlighted, clicking");
                OnClicked();
            }
            
        }
        else if(_controller.TriggerValue==0.0f)
        {
            pressed = false;
        }
    }
  

    public virtual void Highlighted()
    {
        //Debug.Log("Highlighted");
        if (!highlighted)
        {
            highlighted = true;
            _controller.StartFeedbackPatternVibe(MLInputControllerFeedbackPatternVibe.Bump, MLInputControllerFeedbackIntensity.Low);
        }
    }
    public virtual void UnHighlighted()
    {
        //Debug.Log("Unhighlighted");
        if (highlighted)
        {
            highlighted = false;
        }
    }

    protected abstract void OnClicked();
}

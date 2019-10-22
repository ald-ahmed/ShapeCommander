using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class Control6DOF : MonoBehaviour
{
    #region Private Variables
    private MLInputController _controller;
    #endregion

    #region Unity Methods
    void Start()
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
    void Update()
    {
        //Attach the Beam GameObject to the Control
        transform.position = _controller.Position;
        transform.rotation = _controller.Orientation;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
        print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
        print("Their relative velocity is " + collisionInfo.relativeVelocity);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        print(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
    }


    #endregion
}

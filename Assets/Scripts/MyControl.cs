using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MyControl : MonoBehaviour {
    public MLPersistentBehavior persistentBehavior;
    private GameObject _cube, _camera;
    private MLInputController _controller;
  
    private const float _distance = 2.0f;

    public bool allowPlacement = false;
    private bool hasClicked = false;
    private bool unpressed = false;

    private void Start() {
        _cube = GameObject.Find("Game");
        persistentBehavior = _cube.GetComponent<MLPersistentBehavior>();
        _camera = GameObject.Find("Main Camera");
      
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        
    }
    private void OnDestroy() {
        MLInput.Stop();
    }
    private void Update() {
        
        if (allowPlacement)
        {
            if (_controller.TriggerValue > 0.2f&&unpressed)
            {
                hasClicked = true;
               // Vector3 origin = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);
                Vector3 origin = _controller.Position;
                

                _cube.transform.position = origin + GameObject.Find("Beam").transform.forward * _distance;

                //close:
                // _cube.transform.rotation = Quaternion.LookRotation(GameObject.Find("Beam").transform.forward, Vector3.up);
                _cube.transform.rotation = Quaternion.Euler(0, GameObject.Find("Beam").transform.forward.y, 0);
                // _cube.transform.position = origin + _camera.transform.forward * _distance;
                //_cube.transform.rotation = _camera.transform.rotation;
                persistentBehavior.UpdateBinding();
            }
            else if (hasClicked)
            {
                allowPlacement = false;
                Tile[] tiles=_cube.transform.Find("GameObject").GetComponentsInChildren<Tile>();
                foreach(Tile t in tiles)
                {
                    t.PositionUpdated();
                }
            }
            else
            {
                unpressed = true;
            }
        }
    }
}


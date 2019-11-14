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
            if (_controller.TriggerValue > 0.2f)
            {
                hasClicked = true;
                Vector3 origin = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);
                _cube.transform.position = origin + _camera.transform.forward * _distance;
                //_cube.transform.rotation = _camera.transform.rotation;
                //persistentBehavior.UpdateBinding();
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
        }
    }
}


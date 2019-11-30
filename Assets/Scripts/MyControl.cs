using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class MyControl : MonoBehaviour {

    private GameObject _cube;
    private MLInputController _controller;
  
    private const float _distance = 2.0f;

    private bool allowPlacement = true;
    private bool hasClicked = false;
    private bool unpressed = false;

    private void Start() {
        _cube = GameObject.Find("Game");
       
        
      
        MLInput.Start();
        _controller = MLInput.GetController(MLInput.Hand.Left);
        
    }
    private void OnDestroy() {
        MLInput.Stop();
    }
    private void Update() {
        
        if (allowPlacement)
        {
            if (_controller.IsBumperDown&&unpressed)
            {
                Debug.Log("A");
                hasClicked = true;
               // Vector3 origin = new Vector3(_camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);
                Vector3 origin = _controller.Position;
                

                _cube.transform.position = origin + GameObject.Find("Beam").transform.forward * _distance;

                //close:
                // _cube.transform.rotation = Quaternion.LookRotation(GameObject.Find("Beam").transform.forward, Vector3.up);
                _cube.transform.forward = GameObject.Find("Beam").transform.forward;
                Vector3 v = _cube.transform.rotation.eulerAngles;
                _cube.transform.rotation = Quaternion.Euler(0, v.y, 0);//flatten it out
                //_cube.transform.rotation = Quaternion.Euler(0, GameObject.Find("Beam").transform.forward.y, 0);
                // _cube.transform.position = origin + _camera.transform.forward * _distance;
                //_cube.transform.rotation = _camera.transform.rotation;
               
            }
            else if (hasClicked)
            {
                Debug.Log("B");
                allowPlacement = false;
                
                Vector3 v = _cube.transform.rotation.eulerAngles;
                _cube.transform.rotation = Quaternion.Euler(0, v.y, 0);//flatten it out
                
                Tile[] tiles=_cube.transform.Find("Battle Ground/GameGrid").GetComponentsInChildren<Tile>();
                foreach(Tile t in tiles)
                {
                    //t.PositionUpdated();
                    t.PositionUpdatedPlus(v.y);
                }
            }
            else
            {
                Debug.Log("C");
                unpressed = true;
            }
        }
    }
}


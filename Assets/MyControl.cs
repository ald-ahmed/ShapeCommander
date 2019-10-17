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

  private void Start() {
    _cube = GameObject.Find("GameObject");
    _camera = GameObject.Find("Main Camera");

    MLInput.Start();
    _controller = MLInput.GetController(MLInput.Hand.Left);
  }
  private void OnDestroy() {
    MLInput.Stop();
  }
  private void Update() {
    if (_controller.TriggerValue > 0.2f) {
      _cube.transform.position = _camera.transform.position + _camera.transform.forward * _distance;
      _cube.transform.rotation = _camera.transform.rotation;
      persistentBehavior.UpdateBinding();
    }
  }
}


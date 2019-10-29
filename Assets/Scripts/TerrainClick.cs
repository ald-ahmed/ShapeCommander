using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class TerrainClick : Clickable
{
    private Vector3 clickedPoint;
    private GameObject myController;
    private PlayerManager playerManager;
    protected override void OnClicked()
    {
        //get the controller and raycast forward from it until it hits this collider, save that point
        RaycastHit hit;
        Physics.Raycast(_controller.Position, myController.transform.forward,out hit);
        //Debug.DrawRay(_controller.Position, myController.transform.forward);
        //Debug.Log("Terrain clicked: " + hit.point);
        //GameObject.Find("Character").GetComponent<NavMeshAgent>().SetDestination(hit.point);
        playerManager.SendSelectedCharacter(hit.point);
    }

    void Update()
    {
        base.Update();
        //Debug.Log(_controller.Position.ToString());
    }

    void Start()
    {
        base.Start();
        myController = GameObject.Find("Beam");
        playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
    }
}

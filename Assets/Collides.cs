
using UnityEngine;
using System.Collections;

public class Collides : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private GameObject lastHit = null;

    private void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();
    }
    private void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            lineRenderer.SetPosition(1, hit.point);
            if (lastHit != null)
            {
                if (!hit.collider.gameObject.Equals(lastHit))
                {
                    lastHit.GetComponent<Clickable>().UnHighlighted();
                    lastHit = null;//attempted by Chris at midnight
                }
            }


            if (hit.collider.gameObject.GetComponent<Clickable>())
            {
                hit.collider.gameObject.GetComponent<Clickable>().Highlighted();

                lastHit = hit.collider.gameObject;
            }

        }
        else
        {
            if (lastHit != null)
            {
                lastHit.GetComponent<Clickable>().UnHighlighted();
                lastHit = null;//attempted by Chris at midnight
            }
            lineRenderer.SetPosition(1, transform.position + transform.forward * 100);
        }
    }

    /* void OnCollisionEnter(Collision collisionInfo)
     {
         print("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
         print("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
         print("Their relative velocity is " + collisionInfo.relativeVelocity);

         foreach (ContactPoint contact in collisionInfo.contacts)
         {
             Debug.DrawRay(contact.point, contact.normal, Color.white);
         }

     }

     void OnCollisionStay(Collision collisionInfo)
     {
         print(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
     }

     void OnCollisionExit(Collision collisionInfo)
     {
         print(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
     }


     void OnTriggerEnter(Collider other)
     {
         print("Collision detected with trigger object " + other.name);
         if (other.gameObject.GetComponent<Clickable>())
         {
             other.gameObject.GetComponent<Clickable>().Highlighted();
         }
     }

     void OnTriggerStay(Collider other)
     {
         print("Still colliding with trigger object " + other.name);

     }

     void OnTriggerExit(Collider other)
     {
         print(gameObject.name + " and trigger object " + other.name + " are no longer colliding");
         if (other.gameObject.GetComponent<Clickable>())
         {
             other.gameObject.GetComponent<Clickable>().UnHighlighted();
         }
     }*/


}
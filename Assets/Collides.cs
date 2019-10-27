
using UnityEngine;
using System.Collections;

public class Collides : MonoBehaviour
{

    void OnCollisionEnter(Collision collisionInfo)
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
        print(collisionInfo.GetContact(0).point);
    }

    void OnCollisionExit(Collision collisionInfo)
    {
        print(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
    }


    void OnTriggerEnter(Collider other)
    {
        print("Collision detected with trigger object " + other.name);
    }

    void OnTriggerStay(Collider other)
    {
        print("Still colliding with trigger object " + other.name);
    }

    void OnTriggerExit(Collider other)
    {
        print(gameObject.name + " and trigger object " + other.name + " are no longer colliding");
    }


}
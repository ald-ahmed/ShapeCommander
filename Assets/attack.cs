using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{

    public Transform target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void SetTarget(Transform t)
    {
        target = t;
        transform.Find("Sphere").GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Renderer>().enabled = true;
        GetComponent<ParticleSystem>().enableEmission = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            transform.LookAt(target);
            if (Vector3.Distance(transform.position,target.transform.position)<1.0f)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
                gameObject.GetComponent<ParticleSystem>().enableEmission = false;
                transform.Find("Sphere").GetComponent< MeshRenderer>().enabled = false;
            }
        }

    }


}

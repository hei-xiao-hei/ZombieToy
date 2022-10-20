using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    public float launchForce = 8.675309f;
    //public ParticleSystem ps;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
       
        Rigidbody rb = other.GetComponent<Rigidbody>();
        //rb.AddForce(Vector3.up * launchForce,ForceMode.Impulse);
        rb.AddForce(Vector3.up * launchForce);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

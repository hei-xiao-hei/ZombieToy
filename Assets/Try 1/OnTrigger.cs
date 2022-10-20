using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTrigger : MonoBehaviour
{
    Rigidbody cube1;
    public float speed;
    public float li;
    // Start is called before the first frame update
    void Start()
    {
        cube1 = this.GetComponent<Rigidbody>();
        speed = 0.1f;
        li = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.Translate(Vector3.right* speed);
        if (Input.GetMouseButton(0))
        {
            Debug.Log("zhe");
            Debug.Log(gameObject.name);
            cube1.AddForce(Vector3.up * li);
        }
    }
    /*private void OnTriggerStay(Collider other)
    {
        Debug.Log("OnTriggerStay");
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter");
    }*/
}

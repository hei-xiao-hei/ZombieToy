using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightss : MonoBehaviour
{
    public float blikDelyTime = 0.2f;
    public int numberofBlinks = 1000000;
    public Light light;
    public GameObject lightPrefab;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine("lightsss");
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.Space))
        {
            light.enabled = !light.enabled;
        }*/
        StartCoroutine("lightsss");
    }
    IEnumerator lightsss()
    {


        int counter = 0;

        while (counter < numberofBlinks)
        {

            light.intensity = 0;

            yield return new WaitForSeconds(blikDelyTime);

            light.intensity = 1;

            if (light.intensity == 0)

                counter++;

        }
        /*int counter = 0;

        while (counter < numberofBlinks)
        {

            yield return new WaitForSeconds(blikDelyTime);

            light.enabled = !light.enabled;

            if (light.enabled)
            {

                counter++;

            }

        }*/
        

    }
}

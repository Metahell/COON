using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

/*
This script is used to read all the data coming from the device. For instance,
If arduino send ->
								{"1",
								"2",
								"3",}
readQueue() will return ->
								"1", for the first call
								"2", for the second call
								"3", for the thirst call

This is the perfect script for integration that need to avoid data loose.
If you need speed and low latency take a look to wrmhlReadLatest.
*/

public class ReadPort : MonoBehaviour
{
    int CMD;
    public float power; //intensité diode
    public float sensor; //intensité capteur
    public bool jumpPress = false;
    public float buttonPress1 = 0;
    public float buttonPress2 = 0;
    public float buttonPress3 = 0;
    public float buttonPress4 = 0;
    public SerialPort sp = new SerialPort("COM3", 9600);
    void Start()
    {
        sp.Open();
        sp.ReadTimeout = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (sp.IsOpen)
        {
            try
            {
                ReadInfo();
            }
            catch (System.Exception)
            {

            }
        }
    }
    void ReadInfo()
    {
        string value = sp.ReadLine(); //Read the information
        string[] vec3 = value.Split(',');
        sensor = int.Parse(vec3[1])/100;
        power = float.Parse(vec3[0])/100;
        buttonPress1 = float.Parse(vec3[2]);
        buttonPress2 = float.Parse(vec3[3]);
        buttonPress3 = float.Parse(vec3[4]);
        buttonPress4 = float.Parse(vec3[5]);
    }
}

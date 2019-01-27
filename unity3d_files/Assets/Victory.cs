using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Water;

public class Victory : MonoBehaviour
{
    public GameObject theBoat;
    public Canvas winorlose;
    public Material Material;

    // Start is called before the first frame update
    void Start()
    {
        Material.SetVector("_GAmplitude", new Vector4(0, 9, 0, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == theBoat)
        {
            Debug.Log("_dbg Victory!");
        //winorlose.en
        GameObject water = GameObject.Find("Water4Advanced");
        WaterBase wat = water.GetComponent<WaterBase>();
            //Material.SetVector("_GAmplitude", Material.GetVector("_GAmplitude") / 2);
            Material.SetVector("_GAmplitude", new Vector4(0, 0, 0, 0));
            //wat.SetVector("_GAmplitude", wat.GetVector("_GAmplitude") /2);
            //_GAmplitude("WaveAmplitude",Vector)=(0.3,0.35,0.25,0.25)
        }
    }
}

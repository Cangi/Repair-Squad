using System.Collections;
using System.Collections.Generic;
using Microsoft.MixedReality.Toolkit;
using UnityEngine;

public class underwater : MonoBehaviour
{
    public Transform waterHeight;
    public Transform playerUnderwaterCamera;
    public bool isUnderwater;
    private Color normalColor;
    private Color underwaterColor;
    public bool waterRising;
    public float waterLevelMax = 1.5f;
    public float waterLevelMin = 0;
    public float risingSpeed = 1;
    // Use this for initialization
    void Start () {
        normalColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);
        underwaterColor = new Color (0.22f, 0.65f, 0.77f, 0.5f);
    }


    void Update ()
    {
        if (waterRising)
        {
            if (waterHeight.position.y >= waterLevelMax)
            {
                waterRising = false;
            }
            else
            {
                waterHeight.position = new Vector3(waterHeight.position.x,
                    waterHeight.position.y + risingSpeed * Time.deltaTime, waterHeight.position.z);
            }
        }
        else if(!waterRising && waterHeight.position.y > waterLevelMin)
        {
            waterHeight.position = new Vector3(waterHeight.position.x,
                waterHeight.position.y - risingSpeed * Time.deltaTime, waterHeight.position.z);
        }
        float heightWError = waterHeight.position.y + 1;
        if ((playerUnderwaterCamera.position.y < heightWError) != isUnderwater) {
            isUnderwater = playerUnderwaterCamera.position.y < heightWError;
            if (isUnderwater) SetUnderwater ();
            if (!isUnderwater) SetNormal ();
        }
    }
 
    void SetNormal () {
        RenderSettings.fogColor = normalColor;
        RenderSettings.fogDensity = 0.01f;
 
    }
 
    void SetUnderwater () {
        RenderSettings.fogColor = underwaterColor;
        RenderSettings.fogDensity = 0.1f;
 
    }
}

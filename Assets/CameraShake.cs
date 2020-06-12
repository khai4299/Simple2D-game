using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Camera camera;
    float shakeAmount = 0;

    private void Awake()
    {
        if (camera == null)
            camera = Camera.main;
    }
    public void Shake(float amount, float length)
    {
        shakeAmount = amount;
        InvokeRepeating("BeginShake", 0, 0.01f);
        Invoke("StopShake", length);
    }
    void BeginShake()
    {
        if (shakeAmount>0)
        {
            Vector3 camPos = camera.transform.position;
            float shakeAmtX = Random.value * shakeAmount * 2 - shakeAmount;
            float shakeAmtY = Random.value * shakeAmount * 2 - shakeAmount;
            camPos.x += shakeAmtX;
            camPos.y += shakeAmtY;
            camera.transform.position = camPos;
        }
    }
    void StopShake()
    {
        CancelInvoke("BeginShake");
        camera.transform.localScale = Vector3.zero;
    }
}

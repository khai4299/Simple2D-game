using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 200f;
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime*speed);
        Destroy(gameObject, 1f);
    }
}

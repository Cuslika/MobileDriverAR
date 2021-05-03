using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheel : MonoBehaviour
{
    [SerializeField]
    private float speedOffset = 10.0f;
    public float Speed { get; set; }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, speedOffset * Speed * Time.deltaTime);
    }
}

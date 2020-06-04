﻿using UnityEngine;

public class DummyTriggerController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter " + other.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision enter " + collision.gameObject.name);
    }
}
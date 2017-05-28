﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialVelocity : MonoBehaviour {

    public Vector3 velocity;
    public Vector3 initialW;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = velocity;
        rigidBody.angularVelocity = initialW;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AddForce : MonoBehaviour {

    private new Rigidbody rigidbody;

    // Use this for initialization
    void OnEnable () {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.angularVelocity = new Vector3(4f, 0, 0);
	}
	
}

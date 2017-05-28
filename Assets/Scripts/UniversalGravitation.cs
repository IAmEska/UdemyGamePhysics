using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalGravitation : MonoBehaviour {

    private const float bigG = 6.673e-11f; // N * (m/kg)2 [kg m s^-2  m^2 kg^-2 = kg^-1 m^3 s^-2] 

    private PhysicsEngine[] physicsEngineArray;

    // Use this for initialization
    void Start () {
        physicsEngineArray = FindObjectsOfType<PhysicsEngine>(); 
    }
	
    public void AddPhysicsEngine(PhysicsEngine physicsEngine)
    {
        Array.Resize(ref physicsEngineArray, physicsEngineArray.Length + 1);
        physicsEngineArray[physicsEngineArray.Length - 1] = physicsEngine;
    }

	// Update is called once per frame
	void FixedUpdate () {
        CalculateGravity();
	}

    void CalculateGravity()
    {
        foreach (PhysicsEngine physicsEngineA in physicsEngineArray)
        {
            foreach (PhysicsEngine physicsEngineB in physicsEngineArray)
            {
                if (physicsEngineA == physicsEngineB) continue;

                Vector3 offset = physicsEngineA.transform.position - physicsEngineB.transform.position; // direction
                float rSquared = Mathf.Pow(offset.magnitude, 2f); // distance squared
                float gravityMagnitude = bigG * physicsEngineA.mass * physicsEngineB.mass / rSquared;
                Vector3 gravityFeltVector = gravityMagnitude * offset.normalized;
                physicsEngineA.AddForce(-gravityFeltVector);
            }
        }
    }
}

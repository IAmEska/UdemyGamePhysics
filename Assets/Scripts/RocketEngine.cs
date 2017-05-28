using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PhysicsEngine))]
public class RocketEngine : MonoBehaviour {
    public float fuelMass; // [kg]
    public float maxThrust; // kN [kg m s^-2] 

    [Range(0, 1f)]
    public float thrustPercent; // [none] 
    public Vector3 thrustUnitVector; // [none] just pointing direction

    private PhysicsEngine physicsEngine;
    private float currentThrust; // N

	// Use this for initialization
	void Start () {
        physicsEngine = GetComponent<PhysicsEngine>();
        physicsEngine.mass += fuelMass;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(fuelMass > FuelThisUpdate())
        {
            fuelMass -= FuelThisUpdate();
            physicsEngine.mass -= FuelThisUpdate();
            ExertForce();
            physicsEngine.AddForce(thrustUnitVector);
        } else
        {
            Debug.LogWarning("Out of rocket fuel");
        }
	}

    float FuelThisUpdate()
    {
        float exhaustMassFlow;  //  [kg]
        float effectiveExhaustVelocity = 4462f; // [m s^-1] liquid H O

        // thrust = massFlow * exhaustVelocity;
        // massFlow = thrust / exhaustVelocity;

        exhaustMassFlow = currentThrust / effectiveExhaustVelocity;
  
        return exhaustMassFlow * Time.deltaTime; // [kg]
    }

    void ExertForce()
    {
        currentThrust = thrustPercent * maxThrust * 1000f; // we need to recalculate into N thats why * 1000
        Vector3 thrustVector = thrustUnitVector.normalized * currentThrust; // N
        physicsEngine.AddForce(thrustVector);
    }
}

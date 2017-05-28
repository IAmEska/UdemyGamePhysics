using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class PhysicsEngine : MonoBehaviour {

    public float mass; // [kg]
    public Vector3 velocityVector; // avarage velocity this FixedUpdate()  [ms^-1]
    public Vector3 netForceVector; // N [kg m s^-2]
    public bool showTrails;

    private List<Vector3> forceVectorList = new List<Vector3>();
    private LineRenderer lineRenderer;
    
    // Use this for initialization
    void Start () {
        SetupThrustTrails();
    }

    void SetupThrustTrails()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.yellow;
        lineRenderer.endColor = Color.yellow;
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;
        lineRenderer.useWorldSpace = false;
    }

    void FixedUpdate()
    {
        DrawTrails();
        UpdatePosition();
    }

    public void AddForce(Vector3 forceVector)
    {
        forceVectorList.Add(forceVector);
    }
    
    void UpdatePosition()
    {
        //Sum forces a nd clear the list
        netForceVector = Vector3.zero;
        foreach (Vector3 forceVector in forceVectorList)
        {
            netForceVector += forceVector;
        }

        Vector3 accelerationVector = netForceVector / mass;
        velocityVector += accelerationVector * Time.deltaTime;
        transform.position += velocityVector * Time.deltaTime;

        forceVectorList.Clear();
    }
   
    void DrawTrails()
    {
        if (showTrails)
        {
            lineRenderer.enabled = true;
            lineRenderer.numPositions = forceVectorList.Count * 2;
            int i = 0;
            foreach (Vector3 forceVector in forceVectorList)
            {
                lineRenderer.SetPosition(i, Vector3.zero);
                lineRenderer.SetPosition(i + 1, -forceVector);
                i = i + 2;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }
}
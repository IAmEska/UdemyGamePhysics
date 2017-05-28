using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Launcher : MonoBehaviour {

    public float maxLaunchSpeed;
    public AudioClip windUpSound, launchSound;
    public PhysicsEngine ballToLaunch;
    
    private float launchSpeed;
    private AudioSource audioSource;
    private float extraSpeedPerFrame;
    private UniversalGravitation universalGravitation;

	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = windUpSound; // so we know the length of the clip
        extraSpeedPerFrame = (maxLaunchSpeed * Time.fixedDeltaTime) / audioSource.clip.length;
        universalGravitation = FindObjectOfType<UniversalGravitation>();
	}
	
    void OnMouseDown()
    {
        launchSpeed = 0f;
        audioSource.clip = windUpSound;

        InvokeRepeating("IncreaseLaunchSpeed", 0.5f, Time.fixedDeltaTime);
        
        audioSource.Play();
        // increase ball speed to max over a few seconds
        // consider invokerrepeating
    }

    void OnMouseUp()
    {
        CancelInvoke("IncreaseLaunchSpeed");
        audioSource.Stop();
        audioSource.clip = launchSound;
        audioSource.Play();

        // launch the ball
        PhysicsEngine newBall = Instantiate(ballToLaunch) as PhysicsEngine;
        newBall.transform.parent = GameObject.Find("Launched Balls").transform;
        Vector3 launchVelocity = new Vector3(1, 1, 0).normalized * launchSpeed;
        newBall.velocityVector = launchVelocity;
        universalGravitation.AddPhysicsEngine(newBall);
    }

    void IncreaseLaunchSpeed()
    {
        if (launchSpeed == maxLaunchSpeed) return;

        launchSpeed += extraSpeedPerFrame;
        if (launchSpeed > maxLaunchSpeed)
            launchSpeed = maxLaunchSpeed;
        
    }
}

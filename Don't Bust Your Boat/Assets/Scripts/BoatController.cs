using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Rigidbody boatRB;
    public float speed = 2.0f;
    public float turnSpeed = 2.0f;
    public float engineRPM; 
    public float coeffecientOfLiquid;


    private float turnSpeedReduction;
    private bool gas;
    private float timer;
    private Vector3 moveSpeed = Vector3.zero;
    private float tiltReduction;

    // Start is called before the first frame update
    void Start()
    {
        boatRB = GetComponent<Rigidbody>();
        moveSpeed = new Vector3(0f, 0f, -speed);
        timer = 60f / engineRPM;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        HandleInput();
        Engine(engineRPM);
        Vector3 normalBoatVel = boatRB.velocity;
        normalBoatVel.Normalize();
        float dragForceZ = -0.5f*((boatRB.velocity.z * boatRB.velocity.z) * (100/Mathf.Abs(transform.rotation.y - 180)) * coeffecientOfLiquid * normalBoatVel.z);
        float dragForceX = -0.5f*((boatRB.velocity.x * boatRB.velocity.x) * (100/Mathf.Abs(transform.rotation.y - 180)) * coeffecientOfLiquid * normalBoatVel.x);
        
       
        float curVel = Mathf.Abs(boatRB.velocity.x) +  Mathf.Abs(boatRB.velocity.z);
        tiltReduction = Mathf.Clamp(curVel, 0.1f, 2.0f);

        turnSpeedReduction = Mathf.Clamp(curVel, 0.1f, 1.0f);
        boatRB.AddForce(dragForceX, 0f, dragForceZ);
    }

    void HandleInput(){
        if(Input.GetKey("a")){
            boatRB.AddRelativeTorque(-Vector3.up * (turnSpeed * turnSpeedReduction));
            boatRB.AddRelativeTorque(Vector3.forward * tiltReduction);
        }
        
        if(Input.GetKey("d")){
            boatRB.AddRelativeTorque(Vector3.up * (turnSpeed * turnSpeedReduction));
            boatRB.AddRelativeTorque(Vector3.forward * -tiltReduction);
        }

        if(Input.GetKey("space")){
            gas = true;
        }else{
            gas = false;
        }
    }

    void Engine(float engineRPM)
    {
        timer -= Time.fixedDeltaTime;
        if(timer < 0f & gas){
            Debug.Log("adding force ");
            boatRB.AddForce(transform.forward * speed, ForceMode.Acceleration);
            timer = 60 / engineRPM;
        }
    }

}

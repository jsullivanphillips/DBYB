using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Rigidbody boatRB;
    public GameObject backRightFloater;
    public GameObject backLeftFloater;
    public float speed = 2.0f;
    public float turnSpeed = 2.0f;
    public float engineRPM; 
    public float coeffecientOfLiquid;


    private float turnSpeedReduction;
    private bool gas;
    private float timer;
    private Vector3 moveSpeed = Vector3.zero;
    

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
        
       
        turnSpeedReduction = Mathf.Max(Mathf.Abs(boatRB.velocity.x), Mathf.Abs(boatRB.velocity.z));
        turnSpeedReduction = Mathf.Clamp(turnSpeedReduction, 0f, 2f);
        boatRB.AddForce(dragForceX, 0f, dragForceZ);
    }

    void HandleInput(){
        if(Input.GetKey("a")){
            transform.Rotate(0f, -turnSpeed * (turnSpeedReduction/2 + 0.1f), 0f, Space.World);
        }
        
        if(Input.GetKey("d")){
            transform.Rotate(0f, turnSpeed * (turnSpeedReduction /2 + 0.1f), 0f, Space.World);
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

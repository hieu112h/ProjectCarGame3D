﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControll : MonoBehaviour
{
    

    [Header("References")]
    [SerializeField] private Rigidbody carRB;
    [SerializeField] private Transform[] rayPoints;
    [SerializeField] private LayerMask drivable;
    [SerializeField] private Transform accelerationPoint;
    [SerializeField] private GameObject[] tires = new GameObject[4];
    [SerializeField] private GameObject[] frontTireParent = new GameObject[2];
    [SerializeField] private TrailRenderer[] skidMarks = new TrailRenderer[2];
    [SerializeField] private ParticleSystem[] skidSmokes = new ParticleSystem[2]; 
    [SerializeField] private AudioSource skidSound;

    [Header("Suspension Settings")]
    [SerializeField] private float springStiffness;
    [SerializeField] private float damperStiffness;
    [SerializeField] private float restLength;
    [SerializeField] private float springTravel;
    [SerializeField] private float wheelRadius;

    private int[] wheelsIsGround = new int[4];
    private bool IsGrounded = false;

    [Header("Visual")]
    [SerializeField] private float tireRotSpeed = 3000f;
    [SerializeField] private float maxSteeringAngle = 30f;
    [SerializeField] public float minSideSkidVelocity = 30f;//van toc toi thieu de oto phat ra tieng truot
    


    [Header("Input")]
    public float move;
    Vector3 moveInput;
    public float steer;
    Vector3 steerInput;
    float tireRotate;

    [Header("Car Settings")]
    [SerializeField] private float acceleration = 800f;
    [SerializeField] private float maxSpeed = 80f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float steerStrength = 15f;
    [SerializeField] private AnimationCurve turningCurve;
    [SerializeField] private float dragCoefficient = 1f;
    [SerializeField] private float brakingDeceleration = 100f;
    [SerializeField] private float brakingDragCoefficient = 0.5f;

    [Header("Audio")]
    [SerializeField]
    [Range(0, 1)] private float minPitch = 1f;
    [SerializeField]
    [Range(1, 5)] private float maxPitch = 5f;

    public Vector3 currentCarLocalVelocity = Vector3.zero;
    public float carVelocityRatio = 0;
  

    void Start()
    {     
        carRB = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        Suspension();
        GroundCheck();
        CalculateCarVelocity();
        Movement();
        TireVisuals();
        //EngineSound();
    }
    private void Update()
    {
        move = Input.GetAxis("Vertical");
        moveInput = acceleration * move * transform.forward;
        steer = Input.GetAxis("Horizontal");
        steerInput = steerStrength * steer * turningCurve.Evaluate(Mathf.Abs(carVelocityRatio)) * Mathf.Sign(carVelocityRatio) * transform.up;
        tireRotate = tireRotSpeed * move * Time.deltaTime;
    }


    private void GroundCheck()
    {
        int tempGroundWheels = 0;// dem xem co bao nhieu banh xe noi voi mat dat
        for (int i = 0; i < wheelsIsGround.Length; i++)
        {
            tempGroundWheels += wheelsIsGround[i];
        }
        if (tempGroundWheels > 1)//neu so banh xe o tren mat dat lon hon 1 thi xe o tren mat dat
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
    }
    private void CalculateCarVelocity()//ham tinh toan van toc o to
    {
        currentCarLocalVelocity = transform.InverseTransformDirection(carRB.velocity);//van toc cuc bo bien doi nghich dao huong
        carVelocityRatio = currentCarLocalVelocity.z / maxSpeed;
    }

    // Update is called once per frame
    public void Suspension()
    {
            for (int i = 0; i < rayPoints.Length; i++)
            {              
                RaycastHit hit;
                float maxLength = restLength + springTravel; //do dai toi da lo xo dc tinh bang do dai nghi + hanh trinh lo xo
                if (Physics.Raycast(rayPoints[i].position, -rayPoints[i].up, out hit, maxLength + wheelRadius, drivable))//ban tia ray xuong duoi voi do dai bang chieu dai toi da lo xo cong voi ban kinh banh xe
                {
                    wheelsIsGround[i] = 1;
                    float currentSpringLength = hit.distance - wheelRadius;//do dai hien tai cua lo xo lay do dai tia tru di r banh xe
                    float springCompression = (restLength - currentSpringLength) / springTravel;//do nen cua lo xo duoc tinh bang lay do dai nghi tru di do dai hien tai / hanh trinh lo xo tinh ra ti le nen khoang 0-1
                    float springVelocity = Vector3.Dot(carRB.GetPointVelocity(rayPoints[i].position), rayPoints[i].up);//van toc lo xo cho phep chung ta tinh luc giam chan can thiet
                    float dampForce = damperStiffness * springVelocity;// luc chống lại lực lo xo bang van toc lo xo nhan vs do cung van dieu tiet
                    float springForce = springStiffness * springCompression;//luc lo xo duoc tinh bang nhân luc nen lo xo voi do cung lo xo
                    float netForce = springForce - dampForce; //tong luc tac dung len o to bang luc lo xo tru di luc chong lai lo xo
                    carRB.AddForceAtPosition(netForce * rayPoints[i].up, rayPoints[i].position);// tac dung luc lo xo len xe lay luc lo xo huong len tại diem tiep xuc
                    SetTirePosition(tires[i], hit.point + rayPoints[i].up * wheelRadius);
                    Debug.DrawLine(rayPoints[i].position, hit.point, Color.red);
                }
                else
                {
                    wheelsIsGround[i] = 0;
                SetTirePosition(tires[i], rayPoints[i].position - rayPoints[i].up * maxLength);
                    Debug.DrawLine(rayPoints[i].position, rayPoints[i].position + (wheelRadius + maxLength) * -rayPoints[i].up, Color.red); // neu tia ray khong cham vao bat ki thu gi ve ti ray bang do dai toi da cong voi ban kinh xe
                }
            }
        
    }
    public void Movement()
    {
        if (IsGrounded)
        {         
            Acceleration();
            Decelration();
            Turn();
            SidewaysDrags();
            Vfx();
        }
    }
    private void Acceleration()
    {
        if (currentCarLocalVelocity.z < maxSpeed)
        {
            carRB.AddForceAtPosition(moveInput, accelerationPoint.position, ForceMode.Acceleration);
        }
    }
    private void Decelration()
    {
        carRB.AddForceAtPosition((Input.GetKey(KeyCode.Space) ? brakingDeceleration : deceleration) * carVelocityRatio * - carRB.transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }
    private void Turn()//ham re
    {
        carRB.AddTorque(steerInput,ForceMode.Acceleration);
    }
    private void SidewaysDrags()//keos sang mot ben
    {
        float currentSidewaySpeed = currentCarLocalVelocity.x;//xac dinh toc do ngang hien tai
        float dragMagnitude = -currentSidewaySpeed * (Input.GetKey(KeyCode.Space) ? brakingDragCoefficient : dragCoefficient);// luc can lai do lon luc ngang duoc xac dinh
        Vector3 dragForce = transform.right * dragMagnitude; //luc keo theo huong nguoc lai cua xe tranform.right la huong di chuyen cua xe
        carRB.AddForceAtPosition(dragForce, carRB.worldCenterOfMass, ForceMode.Acceleration);
    }
    private void SetTirePosition(GameObject tire,Vector3 targetPosition)//thiet lap vi tri banh xe
    {
        tire.transform.position = targetPosition;
    }
    private void TireVisuals()//hinh anh lop
    {
        float steeringAngle = maxSteeringAngle * steer;//goc lai tuong ung voi dau vao
        for (int i = 0; i < tires.Length; i++)
        {
            if (i < 2)
            {
                tires[i].transform.Rotate(Vector3.right, tireRotSpeed * carVelocityRatio * Time.deltaTime, Space.Self);
                frontTireParent[i].transform.localEulerAngles = new Vector3(frontTireParent[i].transform.localEulerAngles.x, steeringAngle, frontTireParent[i].transform.localEulerAngles.z);
            }
            else
            {
                tires[i].transform.Rotate(Vector3.right, tireRotate, Space.Self);//xoay theo truc x dua vao gia toc dau vao
            }
        }
    }
    private void Vfx()
    {
        if (IsGrounded && Mathf.Abs(currentCarLocalVelocity.x)>minSideSkidVelocity)
        {
            ToggleSkidMarks(true);
            ToggleSkidSound(true);
            ToggleSkidSmoke(true);
        }
        else
        {
            ToggleSkidSmoke(false);
            ToggleSkidMarks(false);
            ToggleSkidSound(false);
        }
    }
    private void ToggleSkidMarks(bool toggle)
    {
        foreach (var skidMark in skidMarks)
        {
            skidMark.emitting = toggle;
        }
    }
    // private void EngineSound()
    // {
    //     engineSound.pitch = Mathf.Lerp(minPitch, maxPitch, Mathf.Abs(carVelocityRatio));
    // }
    private void ToggleSkidSound(bool toggle)
    {
        skidSound.mute = !toggle;
    }
    private void ToggleSkidSmoke(bool toggle)
    {
        foreach (var skidSmoke in skidSmokes)
        {
            if (toggle)
            {
                skidSmoke.Play();
            }
            else
            {
                skidSmoke.Stop();
            }
        }
    }
}

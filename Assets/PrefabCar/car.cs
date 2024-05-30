using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class car : MonoBehaviour
{
    //public TextMeshProUGUI textMeshPro;
    public float time;
    public string name1;
    public LeadBoard leadBoard;
    public Timer timer;
    [Header("Line")]
    [SerializeField] private Transform path;
    [SerializeField] private List<Transform> nodes;
    public int currentNode = 0;
    public bool timeFinish=false;

    [Header("References")]
    [SerializeField] private Rigidbody carRB;
    //[SerializeField] private Transform[] rayPoints;
    //[SerializeField] private LayerMask drivable;
    [SerializeField] private Transform accelerationPoint;
    [SerializeField] private GameObject[] tires = new GameObject[4];
    [SerializeField] private GameObject[] frontTireParent = new GameObject[2];
    //[SerializeField] private TrailRenderer[] skidMarks = new TrailRenderer[2]; 
    //[SerializeField] private ParticleSystem[] skidSmokes = new ParticleSystem[2]; 

    //[Header("Suspension Settings")]
    //[SerializeField] private float springStiffness;
    //[SerializeField] private float damperStiffness;
    //[SerializeField] private float restLength;
    //[SerializeField] private float springTravel;
    //[SerializeField] private float wheelRadius;

    //private int[] wheelsIsGround = new int[4];
    //private bool IsGrounded = false;

    //[Header("Visual")]
    [SerializeField] private float tireRotSpeed = 3000f;
    [SerializeField] private float maxSteeringAngle = 30f;
    [SerializeField] private float brakingDeceleration = 100f;
    [SerializeField] private float brakingDragCoefficient = 0.5f;
    //[SerializeField] private float minSideSkidVelocity = 1f;//van toc toi thieu de oto phat ra tieng truot


    //[Header("Input")]
    //public float move = 3.0f;
    Vector3 moveInput;
    //public float steer;
    //Vector3 steerInput;
    float tireRotate;

    [Header("Car Settings")]
    [SerializeField] private float acceleration = 1000f;
    [SerializeField] private float maxSpeed = 100f;
    [SerializeField] private float deceleration = 10f;
    [SerializeField] private float steerStrength = 400f;
    [SerializeField] private AnimationCurve turningCurve;
    [SerializeField] private float dragCoefficient = 1f;

    private Vector3 currentCarLocalVelocity = Vector3.zero;
    private float carVelocityRatio = 0;
    

    void Start()
    {
        carRB = GetComponent<Rigidbody>();
        nodes = new List<Transform>();
        Transform[] pathTranform = path.GetComponentsInChildren<Transform>();
        for (int i = 0; i < pathTranform.Length; i++)
        {
            if (pathTranform[i] != path.transform)
            {
                nodes.Add(pathTranform[i]);
            }
        }
        name1 = this.gameObject.tag;
    }

    void FixedUpdate()
    {
       
        CalculateCarVelocity();
        Movement();
        
        //int minutes = Mathf.FloorToInt(elapsedTime / 60);
        //int seconds = Mathf.FloorToInt(elapsedTime % 60);
        //textMeshPro.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        //TireVisuals();
    }
    Vector3 turn;

    private void Update()
    {
        CheckWayPointDistance();
        tireRotate = tireRotSpeed * 30f * Time.deltaTime;
        TireVisuals();
    }


    //private void GroundCheck()
    //{
    //    int tempGroundWheels = 0;// dem xem co bao nhieu banh xe noi voi mat dat
    //    for (int i = 0; i < wheelsIsGround.Length; i++)
    //    {
    //        tempGroundWheels += wheelsIsGround[i];
    //    }
    //    if (tempGroundWheels > 1)//neu so banh xe o tren mat dat lon hon 1 thi xe o tren mat dat
    //    {
    //        IsGrounded = true;
    //    }
    //    else
    //    {
    //        IsGrounded = false;
    //    }
    //}
    private void CalculateCarVelocity()//ham tinh toan van toc o to
    {
        currentCarLocalVelocity = transform.InverseTransformDirection(carRB.velocity);//van toc cuc bo bien doi nghich dao huong
        carVelocityRatio = currentCarLocalVelocity.z / maxSpeed;
    }

    // Update is called once per frame
    public void Movement()
    {
        
            Acceleration();
            //Decelration();
            Turn();
            SidewaysDrags();

        
    }
    public void CheckWayPointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 9f)
        {
            moveInput = acceleration * 0.0f * transform.forward;
            if (currentNode == nodes.Count - 1)
            {
                if (!timeFinish)
                {
                    Decelration();
                    time = timer.elapsedTime;
                    leadBoard.AddPlayer(name1,time);
                    timeFinish = true;
                }
                
            }
            else
            {
                currentNode++;
            }

        }
        else
        {
            moveInput = acceleration * 0.15f * transform.forward;
        }
    }
    private void Acceleration()
    {
        //moveInput = acceleration * 0.2f * transform.forward;
        carRB.AddForceAtPosition(moveInput, accelerationPoint.position, ForceMode.Acceleration);
    }
    private void Decelration()
    {
        carRB.AddForceAtPosition(brakingDeceleration * carVelocityRatio * -carRB.transform.forward, accelerationPoint.position, ForceMode.Acceleration);
    }
    public float newSteer;
    private void Turn()//ham re
    {
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        newSteer = (relativeVector.x / relativeVector.magnitude) * 2;


        turn = steerStrength * newSteer * turningCurve.Evaluate(Mathf.Abs(carVelocityRatio)) * Mathf.Sign(carVelocityRatio) * transform.up;
        carRB.AddTorque(turn, ForceMode.Acceleration);
    }
    private void SidewaysDrags()//keos sang mot ben
    {
        float currentSidewaySpeed = currentCarLocalVelocity.x;//xac dinh toc do ngang hien tai
        float dragMagnitude = -currentSidewaySpeed * dragCoefficient;// luc can lai do lon luc ngang duoc xac dinh
        Vector3 dragForce = transform.right * dragMagnitude; //luc keo theo huong nguoc lai cua xe tranform.right la huong di chuyen cua xe
        carRB.AddForceAtPosition(dragForce, carRB.worldCenterOfMass, ForceMode.Acceleration);
    }
    //private void SetTirePosition(GameObject tire, Vector3 targetPosition)//thiet lap vi tri banh xe
    //{
    //    tire.transform.position = targetPosition;
    //}
    private void TireVisuals()//hinh anh lop
    {
        float steeringAngle = maxSteeringAngle * newSteer;//goc lai tuong ung voi dau vao
        //tires[i].transform.Rotate(Vector3.right, tireRotSpeed * carVelocityRatio * Time.deltaTime, Space.Self);
        for (int i = 0; i < tires.Length; i++)
        {
            tires[i].transform.Rotate(Vector3.right, tireRotSpeed * carVelocityRatio * Time.deltaTime, Space.Self);
            //if (i < 2)
            //{
            //    //tires[i].transform.Rotate(Vector3.right, tireRotSpeed * carVelocityRatio * Time.deltaTime, Space.Self);
            //    frontTireParent[i].transform.localEulerAngles = new Vector3(frontTireParent[i].transform.localEulerAngles.x, steeringAngle* Time.deltaTime, frontTireParent[i].transform.localEulerAngles.z);
            //}
            
        }
        for(int i = 0;i<frontTireParent.Length;i++)
        {
            //frontTireParent[i].transform.localEulerAngles = new Vector3(frontTireParent[i].transform.localEulerAngles.x, steeringAngle * Time.deltaTime, frontTireParent[i].transform.localEulerAngles.z);
            frontTireParent[i].transform.Rotate(Vector3.up, steeringAngle * Time.deltaTime, Space.Self);
        }
    }
}

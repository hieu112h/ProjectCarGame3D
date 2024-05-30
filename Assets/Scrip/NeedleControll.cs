using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleControll : MonoBehaviour
{
    public GameObject needle;
    private float startPosition = 220f, endPosition = -40f;
    private float desiredPosition;
    public float verhicalSpeed;
    private CarControll carControll;

    // Start is called before the first frame update
    void Start()
    {
        carControll = GetComponent<CarControll>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        verhicalSpeed = Mathf.Abs(carControll.carVelocityRatio);
        updateNeedle();
    }
    public void updateNeedle()
    {
        desiredPosition = startPosition - endPosition;       
        needle.transform.eulerAngles = new Vector3(0, 0, startPosition - verhicalSpeed * desiredPosition);
    }
}

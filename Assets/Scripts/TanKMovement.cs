using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TanKMovement : MonoBehaviour
{
    //移動速度
    public float moveSpeed;

    //旋回速度
    public float turnSpeed;

    //回避速度
    public float avoidanceSpeed;

    Rigidbody rb;

    //
    float movementInputValue;
    float turnInputValue;
    float avoidanceValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //TankMoveを呼ぶ
        TankMove();
        //TankTurnを呼ぶ
        //TankTurn();

        //TankAvoidanceを呼ぶ
        TankAvoidance();
        //TankAvoidanceL();
    }

    //前進・後退のメソッド
    void TankMove()
    {
        movementInputValue = Input.GetAxis("Vertical");
        Vector3 movement = transform.forward * movementInputValue * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }

    //旋回メソッド
    void TankTurn()
    {
        turnInputValue = Input.GetAxis("Horizontal");
        float turn = turnInputValue * turnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0, turn, 0);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    //回避メソッド
    void TankAvoidance()
    {
        avoidanceValue = Input.GetAxis("Horizontal");
        Vector3 avoid = transform.right * avoidanceValue * avoidanceSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + avoid);
    }

    void TankAvoidanceL()
    {
        Input.GetKeyDown(KeyCode.A);
        Vector3 avoid = -transform.right * avoidanceValue * avoidanceSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + avoid);
    }
}
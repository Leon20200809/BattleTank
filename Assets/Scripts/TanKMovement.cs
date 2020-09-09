using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TanKMovement : MonoBehaviour
{
    //移動速度
    public float moveSpeed;

    //旋回速度
    public float turnSpeed;

    //回避距離
    public float avoiddistance;

    Rigidbody rb;

    //
    float movementInputValue;
    float turnInputValue;

    [SerializeField]
    private AudioClip avoidSound = null;

    //エフェクトプレハブのデータを入れるための箱を作る。
    [SerializeField]
    private GameObject effectPrefab = null;

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
        TankAvoidanceL();
        TankAvoidanceR();
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
    void TankAvoidanceR()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            transform.DOMove(transform.right * avoiddistance, 0.5f).SetRelative();
            AudioSource.PlayClipAtPoint(avoidSound, transform.position);
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
        }
    }

    void TankAvoidanceL()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.DOMove(-transform.right * avoiddistance, 0.5f).SetRelative();
            AudioSource.PlayClipAtPoint(avoidSound, transform.position);
            GameObject effect = Instantiate(effectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 1.0f);
        }
        

    }
}
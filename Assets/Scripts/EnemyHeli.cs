using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeli : MonoBehaviour
{
    public GameObject target;
    public GameObject enemyShotShellA;
    public GameObject enemyShotShellB;
    public GameObject enemyShellPrefab;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // 常にターゲットの方を向く。
        transform.LookAt(target.transform);

        // ターゲットとの距離が離れている場合には、ターゲットに近く。
        if (Vector3.Distance(transform.position, target.transform.position) > 20f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 3);
        }
        else
        {
            // 一定距離に近づいたら機体の高度を上げる。
            if (transform.position.y < 18f)
            {
                transform.Translate(Vector3.up * Time.deltaTime * 3);
            }

            // 攻撃開始
            count += 1;
            if (count % 60 == 0)
            {
                GameObject enemyShellA = Instantiate(enemyShellPrefab, enemyShotShellA.transform.position, enemyShotShellA.transform.rotation);
                GameObject enemyShellB = Instantiate(enemyShellPrefab, enemyShotShellB.transform.position, enemyShotShellB.transform.rotation);
                Rigidbody enemyShellARb = enemyShellA.GetComponent<Rigidbody>();
                Rigidbody enemyShellBRb = enemyShellB.GetComponent<Rigidbody>();
                enemyShellARb.AddForce(transform.forward * 500);
                enemyShellBRb.AddForce(transform.forward * 500);
            }
        }
    }
}

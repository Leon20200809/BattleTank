﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    [SerializeField]
    GameObject effectPrefab1 = null;
    [SerializeField]
    GameObject effectPrefab2 = null;

    public int tankHP;
    [SerializeField]
    private Text HPLabel = null;

    // HPの最大値を設定する（最大値は自由）
    public int tankMaxHP = 10;

    void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    private void OnTriggerEnter(Collider other)
    {
        // もしもぶつかってきた相手のTagが”EnemyShell”であったならば（条件）
        if (other.gameObject.tag == "EnemyShell")
        {
            // HPを１ずつ減少させる。
            tankHP -= 1;
            //UIに反映させる
            HPLabel.text = "HP:" + tankHP;

            // ぶつかってきた相手方（敵の砲弾）を破壊する。
            Destroy(other.gameObject);

            if (tankHP > 0)
            {
                GameObject effect1 = Instantiate(effectPrefab1, transform.position, Quaternion.identity);
                Destroy(effect1, 1.0f);
            }
            else
            {
                GameObject effect2 = Instantiate(effectPrefab2, transform.position, Quaternion.identity);
                Destroy(effect2, 1.0f);

                // プレーヤーを破壊する。
                //Destroy(gameObject);

                // プレーヤーを破壊せずに画面から見えなくする（ポイント・テクニック）
                // プレーヤーを破壊すると、その時点でメモリー上から消えるので、以降のコードが実行されなくなる。
                this.gameObject.SetActive(false);

                // ★追加
                // 1.5秒後に「GoToGameOver()」メソッドを実行する。
                Invoke("GoToGameOver", 1.5f);
            }
        }
    }

    // publicをつけること（重要ポイント）
    public void AddHP(int amount)
    {
        tankHP += amount;

        // 最大HP以上にならないように
        if (tankHP > tankMaxHP)
        {
            tankHP = tankMaxHP;
        }

        HPLabel.text = "HP:" + tankHP;
    }

    // Start is called before the first frame update
    void Start()
    {
        tankHP = tankMaxHP;
        //現HP表示
        HPLabel.text = "ARMOUR : " + tankHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotShell : MonoBehaviour
{
    public float shotSpeed;

    // privateの状態でもInspector上から設定できるようにするテクニック。
    [SerializeField]
    private GameObject shellPrefab = null;

    [SerializeField]
    private AudioClip shotSound = null;

    //発射間隔
    private float timeBetweenShot = 0.75f;
    private float timer;

    //残弾
    public int shotCount;

    [SerializeField]
    Text shellLabel;

    // 残弾数の最大値を設定する（最大値は自由）
    public int shotMaxCount = 50;


    // Start is called before the first frame update
    void Start()
    {
        shotCount = shotMaxCount;
        shellLabel.text = "砲弾：" + shotCount;
    }

    // Update is called once per frame
    void Update()
    {
        // 発射間隔タイマーの時間を動かす
        timer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && timer > timeBetweenShot && shotCount > 0)
        {
            //弾消費
            shotCount -= 1;

            //UI更新
            shellLabel.text = "砲弾：" + shotCount;

            // 発射間隔タイマーの時間を０に戻す。
            timer = 0.0f;

            // 砲弾のプレハブを実体化（インスタンス化）する。
            GameObject shell = Instantiate(shellPrefab, transform.position, Quaternion.identity);

            // 砲弾に付いているRigidbodyコンポーネントにアクセスする。
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();

            // forward（青軸＝Z軸）の方向に力を加える。
            shellRb.AddForce(transform.forward * shotSpeed);

            // 発射した砲弾を３秒後に破壊する。
            // （重要な考え方）不要になった砲弾はメモリー上から削除すること。
            Destroy(shell, 2.0f);

            // 砲弾の発射音を出す。
            AudioSource.PlayClipAtPoint(shotSound, transform.position);
           
        }
    }

    // 残弾数を増加させるメソッド（関数・命令ブロック）
    // 外部からこのメソッドを呼び出せるように「public」をつける（重要ポイント）
    // この「AddShellメソッド」を「ShellItem」スクリプトから呼び出す。
    public void AddShell(int amount)
    {
        // shotCountをamount分だけ回復させる
        shotCount += amount;

        // ただし、残弾数が最大値を超えないようする。(最大値は自由に設定)
        if (shotCount > shotMaxCount)
        {
            shotCount = shotMaxCount;
        }

        // 回復をUIに反映させる。
        shellLabel.text = "砲弾：" + shotCount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    // エフェクトプレハブのデータを入れるための箱を作る。
    [SerializeField]
    private GameObject effectPrefab;

    [SerializeField]
    GameObject effectPrefab2;
    public int objectHP;

    //ドロップアイテム
    [SerializeField]
    private GameObject[] itemPrefabs;

    // これが敵を倒すと得られる点数になる
    [SerializeField]
    private int scoreValue; 
    private ScoreManager sm;

    // このメソッドはぶつかった瞬間に呼び出される
    private void OnTriggerEnter(Collider other)
    {
        
        // もしもぶつかった相手のTagにShellという名前が書いてあったならば
        if (other.CompareTag("Shell"))
        {
            // オブジェクトのHPを１ずつ減少させる。
            objectHP -= 1;

            // まだ破壊できない場合
            if (objectHP > 0)
            {
                Destroy(other.gameObject);
                GameObject effect2 = Instantiate(effectPrefab2, other.transform.position, Quaternion.identity);
                Destroy(effect2, 2.0f);
            }
            // HPが0になった場合
            else
            {
                Destroy(other.gameObject);
                GameObject effect = Instantiate(effectPrefab, other.transform.position, Quaternion.identity);
                Destroy(effect, 2.0f);

                Destroy(this.gameObject);

                Debug.Log("Destroy!");

                // アイテム数が「3個」の場合、itemNumberの中には「0」「1」「2」のいずれかの数字が入る。
                int itemNumber = Random.Range(0, itemPrefabs.Length);

                //アイテムドロップ
                // （ポイント）pos.y + 0.6fの部分でアイテムの出現場所の『高さ』を調整しています。
                Vector3 pos = transform.position;
                if (itemPrefabs.Length != 0)
                {
                    // （ポイント）itemNumberの数字によって出るアイテムが変化する。
                    Instantiate(itemPrefabs[itemNumber], new Vector3(pos.x, pos.y + 0.6f, pos.z), Quaternion.identity);
                }

                //敵撃破スコア加算
                sm.AddScore(scoreValue);

            }

            
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        // 「ScoreLabelオブジェクト」に付いている「ScoreManagerスクリプト」の情報を取得して「sm」の箱に入れる。
        sm = GameObject.Find("ScoreLabel").GetComponent<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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
            }

            
            
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

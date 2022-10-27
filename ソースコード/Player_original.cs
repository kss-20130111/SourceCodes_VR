using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.XR;
using UnityEngine.UI;

public class Player_original : MonoBehaviour 
{
    //変数宣言
    public int Player_Hp;
    public int Player_Attack;
    public float MutekiTime;

    private Enemy enemy;
    public GameObject playerHP1;
    public GameObject playerHP2;
    public GameObject playerHP3;
    public GameObject playerHP4;
    public GameObject playerHP5;

    GameObject headCollider;
    SceneChanger sceneChanger;
    //被ダメージ時のUI表示用
    public Image Qimage;
    bool a_flag;
    public float timer = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        //headCollider初期化
        headCollider = transform.Find("VRCamera/FollowHead/HeadCollider").gameObject;
        //無敵時UIをfalse
        a_flag = false;
        Qimage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //被ダメージ時のUIがtrueになったら
        if (a_flag == true)
        {
            //時間経過で薄くしていく
            timer -= Time.deltaTime;
            Qimage.color -= new Color(0, 0, 0, Time.deltaTime);
            if (timer <= 0)
            {
                //時間経過をリセット
                timer = 1.5f;
            }
        }
    }

    //プレイヤー体力
    public void PlayerHp(int Attack)
    {
        Player_Hp -= Attack;
        if(Player_Hp == 4)
        {
            //無敵オン
            MutekiManager();
            Destroy(playerHP5);
        }
        else if (Player_Hp == 3)
        {
            //無敵オン
            MutekiManager();
            Destroy(playerHP4);
        }
        else if(Player_Hp == 2)
        {
            //無敵オン
            MutekiManager();
            Destroy(playerHP3);
        }
        else if(Player_Hp == 1)
        {
            //無敵オン
            MutekiManager();
            Destroy(playerHP2);
        }
        else if(Player_Hp <= 0)
        {
            //無敵オン
            MutekiManager();
            Destroy(playerHP1);
            //体力ゼロでゲームオーバーシーンへ移行
            sceneChanger = GameObject.Find("SceneManager").GetComponent<SceneChanger>();
            sceneChanger.GameOver();
        }
    }

    public void AttackUp()
    {
        //コンボで攻撃力アップ
        Player_Attack += 5;
    }

    /// <summary>
    /// 以下担当箇所
    /// </summary>
    private void MutekiManager()
    {
        //無敵をOnにして被ダメージUIをOn
        Qimage.gameObject.SetActive(true);
        a_flag = true;
        headCollider.layer = LayerMask.NameToLayer("MutekiNow");
        Invoke("MutekiOff", MutekiTime);
    }
}

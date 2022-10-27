using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
//using KanKikuchi.AudioManager;

public class BeamManager : MonoBehaviour
{
    //タイトル画面剣取得
    GameObject BeamSwordTitle;
    public GameObject Sword_Light_Title;

    //メインゲーム_Right剣取得
    GameObject BeamSwordMain_Right;
    GameObject Sword_Light_Main_Right;
    //メインゲーム_Left剣取得
    GameObject BeamSwordMain_Left;
    GameObject Sword_Light_Main_Left;

    //Intractable取得
    Interactable interactable_Right;
    Interactable interactable_Left;
    
    //Animator取得
    public Animator animator_Title;
    Animator animator_Main_Right;
    Animator animator_Main_Left;

    private SEManager SEManager;
    private void Start()
    {
        //各シーンで剣オブジェクト・Animator・Interactable初期化
        if (SceneManager.GetActiveScene().name == "Title" ||
            SceneManager.GetActiveScene().name == "GameOver" ||
            SceneManager.GetActiveScene().name == "GameClear")
        {
            BeamSwordTitle = GameObject.Find("EnergySwordS01_M1_Title");
            Sword_Light_Title = BeamSwordTitle.transform.Find("LightSaber_Light").gameObject;
            animator_Title = BeamSwordTitle.GetComponent<Animator>();
        }
        
        if (SceneManager.GetActiveScene().name == "Stage1" ||
            SceneManager.GetActiveScene().name=="Stage2"||
            SceneManager.GetActiveScene().name=="Stage3"||
            SceneManager.GetActiveScene().name == "Tutorial")
        {
            BeamSwordMain_Right = GameObject.Find("EnergySwordS01_M1_Right");
            BeamSwordMain_Left = GameObject.Find("EnergySwordS01_M1_Left");

            Sword_Light_Main_Right = BeamSwordMain_Right.transform.Find("LightSaber_Light").gameObject;
            Sword_Light_Main_Left = BeamSwordMain_Left.transform.Find("LightSaber_Light").gameObject;

            animator_Main_Right = BeamSwordMain_Right.GetComponent<Animator>();
            animator_Main_Left = BeamSwordMain_Left.GetComponent<Animator>();

            interactable_Right = BeamSwordMain_Right.GetComponent<Interactable>();
            interactable_Left = BeamSwordMain_Left.GetComponent<Interactable>();
        }


        SEManager = GameObject.Find("SEManager").GetComponent<SEManager>();
    }

    private void SE_Play()
    {
        SEManager.PlaySE(SEManager.BeamClip);
    }

    //手に掴んだら呼び出しビーム演出をOn
    public void BeamOn()
    {
        if (SceneManager.GetActiveScene().name == "Stage1" ||
            SceneManager.GetActiveScene().name == "GameOver" ||
            SceneManager.GetActiveScene().name == "GameClear")
        {
            //オブジェクトのSetActiveをtrue＋音とアニメーション
            Sword_Light_Title.SetActive(true);
            SE_Play();
            animator_Title.Play("EnergySword_Grab");
        }
    }
    //手に掴んだら呼び出しビーム演出をOn
    public void LeftBeamOn()
    {
        if (SceneManager.GetActiveScene().name == "Stage1" ||
            SceneManager.GetActiveScene().name=="Stage2"||
            SceneManager.GetActiveScene().name=="Stage3"||
            SceneManager.GetActiveScene().name=="Tutorial")
        {
            if (interactable_Left.sword_Left == true)
            {
                //オブジェクトのSetActiveをtrue＋音とアニメーション
                Sword_Light_Main_Left.SetActive(true);
                SE_Play();
                animator_Main_Left.Play("EnergySword_Grab");
            }
        }
    }
    //手に掴んだら呼び出しビーム演出をOn
    public void RightBeamOn()
    {
        if (SceneManager.GetActiveScene().name == "Stage1" ||
            SceneManager.GetActiveScene().name=="Stage2"||
            SceneManager.GetActiveScene().name=="Stage3"||
            SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (interactable_Right.sword_Right == true)
            {
                //オブジェクトのSetActiveをtrue＋音とアニメーション
                Sword_Light_Main_Right.SetActive(true);
                SE_Play();
                animator_Main_Right.Play("EnergySword_Grab");
            }
        }
    }
    //手から離したら呼び出しビーム演出をOff
    public void BeamOff()
    {
        if (SceneManager.GetActiveScene().name == "Title" ||
            SceneManager.GetActiveScene().name == "GameOver" ||
            SceneManager.GetActiveScene().name == "GameClear")
        {
            //オブジェクトのSetActiveをfalse
            Sword_Light_Title.SetActive(false);
        }
    }
    //手から離したら呼び出しビーム演出をOff
    public void LeftBeamOff()
    {
        if (SceneManager.GetActiveScene().name == "Stage1" ||
            SceneManager.GetActiveScene().name == "Stage2" ||
            SceneManager.GetActiveScene().name == "Stage3"||
            SceneManager.GetActiveScene().name == "Tutorial")
        {
            //オブジェクトのSetActiveをfalse
            Sword_Light_Main_Left.SetActive(false);
        }
    }
    //手から離したら呼び出しビーム演出をOff
    public void RightBeamOff()
    {
        if (SceneManager.GetActiveScene().name == "Stage1" ||
            SceneManager.GetActiveScene().name == "Stage2" ||
            SceneManager.GetActiveScene().name == "Stage3"|| 
            SceneManager.GetActiveScene().name=="Tutorial")
        {
            //オブジェクトのSetActiveをfalse
            Sword_Light_Main_Right.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;
using UnityEngine.XR;

public class Controller : MonoBehaviour
{
    public RaycastHit hit;

    //コンボ判定用
    private ComboSystem comboSystem;
    //
    public SteamVR_Input_Sources handtype;
    public SteamVR_Action_Vibration hapicAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Hapic");
    VelocityEstimator VE;

    [SerializeField] GameObject sword;
    Vector3 swordVelocity;
    public bool slash = false;
    public bool toRight = false;
    public bool toLeft = false;
    public bool toTop = false;
    public bool toBottom = false;
    public bool toTopRight = false;
    public bool toTopLeft = false;
    public bool toBottomRight = false;
    public bool toBottomLeft = false;

    
    //InteractUIボタン（初期設定はトリガー）が押されてるのかを判定するためのIuiという関数にSteamVR_Actions.default_InteractUIを固定
    //private SteamVR_Action_Boolean InteractTrigger = SteamVR_Actions.default_InteractUI;
    //結果の格納用Boolean型関数TriggerOn
    private bool TriggerOn;

    public GameObject originObject;

    public Vector3 EnemySetPos;
    private void Start()
    {
        comboSystem = GameObject.Find("GameManager").GetComponent<ComboSystem>();
    }

    private void Update()
    {
        //コンボ判定用
        VE = GetComponent<VelocityEstimator>();
        swordVelocity = VE.GetVelocityEstimate();
   
        Vector3 origin = new Vector3(transform.position.x, transform.position.y, transform.position.z); //原点
        Vector3 direction = transform.up;
        Ray ray = new Ray(origin, direction);
        Physics.Raycast(ray, out hit);
    }

    public void Slash()
    {
        slash = true;
    }

    public void SlashDirection()
    {
        if (swordVelocity.x >= 1.5f)
        {
            if (swordVelocity.y <= 1.8f && swordVelocity.y >= -1.8f)
            {
                //右へ
                toRight = true;
                Slash();
                //コンボ判定用
                comboSystem.RightSlash();
                Instantiate(originObject, new Vector3(hit.point.x , hit.point.y, hit.point.z), Quaternion.Euler(0f, 90f,0f));
            }
            else if (swordVelocity.y > 1.8f)
            {
                //右下へ
                toBottomRight = true;
                Slash();
                //コンボ判定用
                comboSystem.Up_RightSlash();
                Instantiate(originObject, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(-45f, 90f, 0f));
            }
            else
            {
                //右上へ
                toTopRight = true;
                Slash();
                //コンボ判定用
                comboSystem.Down_RightSlash();
                Instantiate(originObject, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(45f, 90f, 0f));
            }
        }
        else if (swordVelocity.x <= -1.5f)
        {
            if (swordVelocity.y <= 1.8f && swordVelocity.y >= -1.8f)
            {
                //左へ
                toLeft = true;
                Slash();
                //コンボ判定用
                comboSystem.LeftSlash();
                Instantiate(originObject, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(0f, -90f, 0f));
            }
            else if (swordVelocity.y < -1.8f)
            {
                //左下へ
                toBottomLeft = true;
                Slash();
                //コンボ判定用
                comboSystem.Down_LeftSlash();
                Instantiate(originObject, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(45f, -90f, 0f));
            }
            else
            {
                //左上へ
                toTopLeft = true;
                Slash();
                //コンボ判定用
                comboSystem.Up_LeftSlash();
                Instantiate(originObject, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(-45f, -90f, 0f));
            }
        }
        else
        {
            if (swordVelocity.y >= 1.7f)
            {
                //上へ
                toTop = true;
                Slash();
                //コンボ判定用
                comboSystem.UpSlash();
                Instantiate(originObject, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(-90f, 0f, 0f));
            }
            else if (swordVelocity.y <= -1.7f)
            {
                //下へ
                toBottom = true;
                Slash();
                //コンボ判定用
                comboSystem.DownSlash();
                Instantiate(originObject, new Vector3(hit.point.x, hit.point.y, hit.point.z), Quaternion.Euler(90f, 0f, 0f));
            }
            else
            {
                //全てfalse
                slash = false;
                toRight = false;
                toLeft = false;
                toTop = false;
                toBottom = false;
                toTopRight = false;
                toTopLeft = false;
                toBottomRight = false;
                toBottomLeft = false;
            }
        }
    }

    

}

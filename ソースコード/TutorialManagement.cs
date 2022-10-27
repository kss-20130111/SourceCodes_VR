using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//チュートリアルステート管理用Enum
public enum State
{
    Start,
    Rule,
    Enemy,
    Combo,
    Range,
    End
}


public class TutorialManagement : MonoBehaviour
{
    //StateEnum取得
    State state;
    //各種Enemy情報取得
    Tutorial_EnemyStatus enemyStatus;
    EnemyBody enemyBody;
    TutorialRangeEnemyController tutorialRangeEnemyController;
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject rangeEnemyBulletPos;
    [SerializeField] GameObject rangeEnemyDrone;
    [SerializeField] GameObject rangeEnemy;
    //ルール説明用UIテキスト
    //ルール説明
    [SerializeField] GameObject howToRule;

    //敵説明用UIテキスト
    [SerializeField] GameObject howToEnemy;

    //コンボの説明用UIテキスト
    //コンボ説明
    [SerializeField] GameObject howToCombo;

    //遠距離の敵説明用UIテキスト
    [SerializeField] GameObject howToRangeEnemy;

    //チュートリアル開始用
    [SerializeField] GameObject TutorialBeginText;

    //チュートリアル終了用
    [SerializeField] GameObject TutorialEndText;

    //チュートリアル開始用テキスト表示用bool
    [SerializeField] bool isTutorialStart;
    //チュートリアル終了用テキスト表示用bool
    [SerializeField] bool isTutorialEnd;
    //ルール説明用テキスト表示用bool
    [SerializeField] bool isHowToRule;
    //近距離敵用テキスト表示用bool
    [SerializeField] bool isHowToEnemy;
    //コンボ用テキスト表示用bool
    [SerializeField] bool isHowToCombo;

    //遠距離敵用テキスト表示用bool
    [SerializeField] bool isHowToRange;

    private void Awake()
    {
        //チュートリアル用Enemy関連初期化
        enemyStatus = rangeEnemyDrone.GetComponent<Tutorial_EnemyStatus>();
        enemyBody = enemy.GetComponent<EnemyBody>();
        tutorialRangeEnemyController = rangeEnemyBulletPos.GetComponent<TutorialRangeEnemyController>();
    }
    private void Start()
    {
        //近距離・遠距離ともに開始時はSetActiveをfalse
        enemy.SetActive(false);
        rangeEnemy.SetActive(false);

        //チュートリアル開始用bool初期化
        isTutorialStart = true;
        //チュートリアル終了用bool初期化
        isTutorialEnd = false;
        //ルール説明用テキスト表示用bool初期化
        isHowToRule = false;
        //近距離敵用テキスト表示用bool初期化
        isHowToEnemy = false;
        //コンボ用テキスト表示用bool初期化
        isHowToCombo = false;
        //遠距離敵用テキスト表示用bool初期化
        isHowToRange = false;
    }

    //UpdateメソッドのInvoke内で呼び出し
    public void TutorialStartEnd()
    {
        isTutorialStart = false;
        isHowToRule = true;
        TutorialBeginText.SetActive(false);
    }
    //UpdateメソッドのInvoke内で呼び出し
    public void HowToRuleEnd()
    {
        isHowToRule = false;
        isHowToEnemy = true;
        howToRule.SetActive(false);
    }
    //UpdateメソッドのInvoke内で呼び出し
    public void HowToEnemyEnd()
    {
        isHowToEnemy = false;
        isHowToCombo = true;
        howToEnemy.SetActive(false);
    }
    //UpdateメソッドのInvoke内で呼び出し
    public void HowToComboEnd()
    {
        isHowToCombo = false;
        isHowToRange = true;
        howToCombo.SetActive(false);
        Destroy(enemy);
    }

    public void HowToRangeEnemyEnd()
    {
        isHowToRange = false;
        isTutorialEnd = true;
        howToRangeEnemy.SetActive(false);
        rangeEnemy.SetActive(false);
    }

    public void TutorialEnd()
    {
        SceneManager.LoadScene("Title");
    }

    void Update()
    {
        //ステートチェック呼び出し
        StateCheck();

        //ステート切り替え
        switch (state)
        {
            case State.Start:
                {
                    TutorialBeginText.SetActive(true);
                    Invoke("TutorialStartEnd", 5.0f);
                }
                break;
            case State.Rule:
                {
                    howToRule.SetActive(true);
                    Invoke("HowToRuleEnd", 5.0f);
                }
                break;
            case State.Enemy:
                {
                    howToEnemy.SetActive(true);
                    EnemySpawn();

                    if (!enemyBody.OnMove)
                    {
                        Invoke("HowToEnemyEnd", 8.0f);
                    }
                }
                break;
            case State.Combo:
                {
                    howToCombo.SetActive(true);
 
                    if (enemyBody.isDie)
                    {
                        Invoke("HowToComboEnd", 3.0f);
                    }

                }
                break;
            case State.Range:
                {
                    howToRangeEnemy.SetActive(true);
                    RangeEnemySpawn();
                    UnRefRangeEnemyMove();
                    if(tutorialRangeEnemyController.count <= 1)
                    {
                        UnRefRangeEnemyMove();
                    }
                    else if(tutorialRangeEnemyController.count >= 2)
                    {
                        RefRangeEnemyMove();
                    }
                    if(enemyStatus.isDead)
                    {
                        Invoke("HowToRangeEnemyEnd",2.0f);
                    }
                }
                break;
            case State.End:
                {
                    TutorialEndText.SetActive(true);
                    Invoke("TutorialEnd", 5.0f);
                }
                break;
        }
    }

    //Enemyをスポーンさせる
    void EnemySpawn()
    {
        enemy.SetActive(true);
    }

    //遠距離Enemyをスポーンさせる
    void RangeEnemySpawn()
    {
        //生む
        rangeEnemy.SetActive(true);
    }

    //跳ね返せない遠距離の敵を出現させる
    void UnRefRangeEnemyMove()
    {
        //遠距離敵のEnum切り替え
        tutorialRangeEnemyController.type = TutorialRangeEnemyController.Type.Unreflectable;

    }
    //跳ね返せる遠距離の敵を出現させる
    void RefRangeEnemyMove()
    {
        //遠距離敵のEnum切り替え
        tutorialRangeEnemyController.type = TutorialRangeEnemyController.Type.Reflectable;
    }

    //各種条件をチェックしてステートを切り替える
    void StateCheck()
    {
        if(isTutorialStart && !isTutorialEnd && !isHowToRule && !isHowToEnemy &&
            !isHowToCombo && !isHowToRange)
        {
            state = State.Start;
        }
        if (!isTutorialStart && !isTutorialEnd && isHowToRule && !isHowToEnemy &&
            !isHowToCombo && !isHowToRange)
        {
            state = State.Rule;
        }
        if (!isTutorialStart && !isTutorialEnd && !isHowToRule && isHowToEnemy &&
            !isHowToCombo && !isHowToRange)
        {
            state = State.Enemy;
        }
        if (!isTutorialStart && !isTutorialEnd && !isHowToRule && !isHowToEnemy &&
            isHowToCombo && !isHowToRange)
        {
            state = State.Combo;
        }
        if (!isTutorialStart && !isTutorialEnd && !isHowToRule && !isHowToEnemy &&
            !isHowToCombo && isHowToRange)
        {
            state = State.Range;
        }
        if (!isTutorialStart && isTutorialEnd && !isHowToRule && !isHowToEnemy &&
            !isHowToCombo && !isHowToRange)
        {
            state = State.End;
        }
    }
}

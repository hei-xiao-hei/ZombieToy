using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_My : MonoBehaviour
{
    public static GameManager_My Instance;//单例模式

    //玩家和敌人的属性
    [Header("Player and Enemy Properties")]
    public PlayerHealth_My Player;
    public Transform EnemyTarget;//敌人的目标位置

    [SerializeField] float delayOnPlayerDeath = 1f;//角色死亡等待时间

    //控制UI
    [Header("UI Properties")]
    [SerializeField] Text infoText;//标题文本
    [SerializeField] Image gameoverImage;//游戏结束UI
    [SerializeField] Button PauseBtn;//暂停按钮
    [SerializeField] Canvas PauseCan;//暂停画布
    [SerializeField] Button ExitButton;//退出按钮

    //播放器的选择属性
    [Header("Player Selection Properties")]
    [SerializeField] GameObject enemySpawners;
    [SerializeField] Animator cameraAnimator;//相机动画

    //羊的属性
    [Header("Ally Properties")]
    [SerializeField] AllyManager_My allyManager;

    //是否选择了玩家
    //public bool IsChosen;
   

    int score = 0;//用于统计得分

    private void Start()
    {
        //场景改变时不销毁此对象
       // DontDestroyOnLoad(gameObject);
        PlayerMovement_My.Instance.Defeated();//开始时canMove设置为false角色不可移动
        
    }

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        else if(Instance!=this)
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayerChosen(PlayerHealth_My selected)
    {
        //选择了玩家
        //IsChosen = true;//y用于控制玩家看向鼠标，开始游戏时，如果没选择角色，则玩家不看向鼠标，当选择角色后玩家看向鼠标

        Player = selected;

        Player.gameObject.GetComponent<PlayerMovement_My>().canMove = true;//选择的角色的canMove设置为true，角色可移动

        EnemyTarget = Player.transform;//玩家的位置是敌人的目标位置

        //TO DO 的得分和敌人UI
        if(infoText!=null)
        {
            infoText.text = "Score:0";
        }
        if(enemySpawners!=null)
        {
            enemySpawners.SetActive(true);
        }

        //播放相机旋转的动画
        if(cameraAnimator!=null)
        {
            cameraAnimator.SetTrigger("Start");
        }


    }
    
    //玩家死亡
    public void PlayerDied()
    {
        EnemyTarget = null;//敌人目标位置清空

        //显示游戏结束UI

        if(gameoverImage!=null)
        {
            gameoverImage.gameObject.SetActive(true);//显示游戏结束界面
        }
    }

    public void PlayerDeathComplete()
    {
        Invoke("ReloadScene", delayOnPlayerDeath);//延时delayOnPlayerDeath后重新加载场景。
    }

    public void AddScore(int points)
    {
        score += points;
        //显示得分UI
        if(infoText!=null)
        {
            infoText.text = "Score:" + score;
        }
    }


    //重新加载场景
    void  ReloadScene()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(currentScene.buildIndex);
        SceneManager.LoadScene("MySelfScene");
    }

    //按下暂停按钮触发事件
    public void Pausebtn()
    {
        PauseCan.gameObject.SetActive(true);

        Time.timeScale = 0;
        
    }

    //退出游戏
    public void ExitGame()
    {
        Application.Quit();
    }
}

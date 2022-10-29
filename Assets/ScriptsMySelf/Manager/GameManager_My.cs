using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager_My : MonoBehaviour
{
    public static GameManager_My Instance;//����ģʽ

    //��Һ͵��˵�����
    [Header("Player and Enemy Properties")]
    public PlayerHealth_My Player;
    public Transform EnemyTarget;//���˵�Ŀ��λ��

    [SerializeField] float delayOnPlayerDeath = 1f;//��ɫ�����ȴ�ʱ��

    //����UI
    [Header("UI Properties")]
    [SerializeField] Text infoText;//�����ı�
    [SerializeField] Image gameoverImage;//��Ϸ����UI
    [SerializeField] Button PauseBtn;//��ͣ��ť
    [SerializeField] Canvas PauseCan;//��ͣ����
    [SerializeField] Button ExitButton;//�˳���ť

    //��������ѡ������
    [Header("Player Selection Properties")]
    [SerializeField] GameObject enemySpawners;
    [SerializeField] Animator cameraAnimator;//�������

    //�������
    [Header("Ally Properties")]
    [SerializeField] AllyManager_My allyManager;

    //�Ƿ�ѡ�������
    //public bool IsChosen;
   

    int score = 0;//����ͳ�Ƶ÷�

    private void Start()
    {
        //�����ı�ʱ�����ٴ˶���
       // DontDestroyOnLoad(gameObject);
        PlayerMovement_My.Instance.Defeated();//��ʼʱcanMove����Ϊfalse��ɫ�����ƶ�
        
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
        //ѡ�������
        //IsChosen = true;//y���ڿ�����ҿ�����꣬��ʼ��Ϸʱ�����ûѡ���ɫ������Ҳ�������꣬��ѡ���ɫ����ҿ������

        Player = selected;

        Player.gameObject.GetComponent<PlayerMovement_My>().canMove = true;//ѡ��Ľ�ɫ��canMove����Ϊtrue����ɫ���ƶ�

        EnemyTarget = Player.transform;//��ҵ�λ���ǵ��˵�Ŀ��λ��

        //TO DO �ĵ÷ֺ͵���UI
        if(infoText!=null)
        {
            infoText.text = "Score:0";
        }
        if(enemySpawners!=null)
        {
            enemySpawners.SetActive(true);
        }

        //���������ת�Ķ���
        if(cameraAnimator!=null)
        {
            cameraAnimator.SetTrigger("Start");
        }


    }
    
    //�������
    public void PlayerDied()
    {
        EnemyTarget = null;//����Ŀ��λ�����

        //��ʾ��Ϸ����UI

        if(gameoverImage!=null)
        {
            gameoverImage.gameObject.SetActive(true);//��ʾ��Ϸ��������
        }
    }

    public void PlayerDeathComplete()
    {
        Invoke("ReloadScene", delayOnPlayerDeath);//��ʱdelayOnPlayerDeath�����¼��س�����
    }

    public void AddScore(int points)
    {
        score += points;
        //��ʾ�÷�UI
        if(infoText!=null)
        {
            infoText.text = "Score:" + score;
        }
    }


    //���¼��س���
    void  ReloadScene()
    {
        //Scene currentScene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(currentScene.buildIndex);
        SceneManager.LoadScene("MySelfScene");
    }

    //������ͣ��ť�����¼�
    public void Pausebtn()
    {
        PauseCan.gameObject.SetActive(true);

        Time.timeScale = 0;
        
    }

    //�˳���Ϸ
    public void ExitGame()
    {
        Application.Quit();
    }
}

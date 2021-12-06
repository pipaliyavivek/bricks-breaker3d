    using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject BallPrefab;

    public int mBallMoveSpeed =8;
    public Level m_Level;

    [SerializeField] public List<GameObject> Balls = new List<GameObject>();
    [SerializeField] public List<GameObject> Levels = new List<GameObject>();

    public GameObject Point;
    private Vector3 OldPosition;
    public bool IsStart;
    bool isdown;
    public int NoOf;

   [SerializeField,ReadOnly] public int lvlNo = 0;
    void Awake() => Instance = this;
    void OnEnable()
    {
        UIManager.TapStart += StartGame;
    }
    void OnDisable()
    {
        UIManager.TapStart -= StartGame;
    }
    void StartGame()
    {
        GameManager.Instance.IsStart = true;
        LoadLevel(lvlNo);
        m_Level = FindObjectOfType<Level>();
        NoOf = 10;
    }
    [Button]
    void shoot()
    {
        Balls.Clear();
        for (int i = 0; i < 10; i++)
        {
            GameObject m_Ball = Instantiate(BallPrefab, Point.transform.GetChild(0).position,Quaternion.identity);
            Balls.Add(m_Ball);
            m_Ball.SetActive(false);
            m_Ball.transform.SetParent(Point.transform.GetChild(0).transform);
        }
    }
    IEnumerator ActiveBalls()
    {
        for (int i = 0; i < NoOf; i++)
        {
            Balls[i].SetActive(true);
            Balls[i].transform.SetParent(transform);
            yield return new WaitForSeconds(0.05f);
           // GameObject m_Ball = Instantiate(BallPrefab, Point.transform.GetChild(0).position, Quaternion.identity);
           // m_Ball.transform.SetParent(Point.transform.GetChild(0).transform);
        }
        DOVirtual.DelayedCall(0.5f,()=> 
        {
            Point.transform.GetChild(0).transform.localEulerAngles = Vector3.zero;
        });
    }
    void FixedUpdate()
    {
        if (!IsStart) return;
        foreach (GameObject SBall in Balls)
        {
            if (SBall.activeSelf)
            {
                return;
            }
        }
        if (Input.GetMouseButton(0))
        {
           Point.transform.GetChild(0).GetComponent<LineRenderer>().enabled = true;
           Point.transform.GetChild(0).transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X")*2f, 0));
        }
        if (Input.GetMouseButtonUp(0))
        {
            Point.transform.GetChild(0).GetComponent<LineRenderer>().enabled = false;
            StartCoroutine(ActiveBalls());
        }
    }
    [Button]
    void LoadLevel(int LevelNo = 0)
    {
       GameObject lvl = Instantiate(Levels[LevelNo]);
       lvl.transform.SetParent(transform.GetChild(0));
       lvl.transform.localPosition = Vector3.zero;
    }
    void DragObj()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OldPosition = Input.mousePosition;
            isdown = true;
        }
        else if (Input.GetMouseButton(0) && isdown)
        {
            if (OldPosition != Vector3.zero)
            {
                var po = Input.mousePosition.x - OldPosition.x;
                var canpos = Point.transform.eulerAngles;
                canpos.x = 0;
                canpos.y = Mathf.Clamp(po,-170,-20);
                canpos.z = 90;
                Point.transform.eulerAngles = canpos;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            isdown = false;
        }
    }
}

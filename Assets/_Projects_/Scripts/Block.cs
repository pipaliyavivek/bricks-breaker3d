using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;

public class Block : MonoBehaviour
{
    [SerializeField] internal int BlockCount;
    public int SingleBlockPower = 5;
    public GameObject m_Block;
    public List<Transform> Blocks = new List<Transform>();

    [SerializeField] TextMeshPro m_TextPower;

    void OnEnable()
    {
        StartGame();
    }
    void OnDisable()
    {
        Blocks.Clear();
    }
    void StartGame()
    {
        MakeBuilding();
        ChangeBlockColor();
        FindTopBlock().gameObject.transform.GetChild(2).gameObject.SetActive(true);
        m_TextPower = FindTopBlock().gameObject.transform.GetChild(2).gameObject.GetComponent<TextMeshPro>();
        m_TextPower.text = BlockCount.ToString();
    }
    public Transform FindDownBlock()
    {
        return Blocks.Find(x => x.transform.position.y > -4f);
    }
    public Transform FindTopBlock()
    {
        return Blocks.FindLast(x => x.transform);
    }
    [Button]
    public void ChangeBlockColor()
    {
        Color[] colors = new Color[6];
        colors[0] = Color.cyan;
        colors[1] = Color.red;
        colors[2] = Color.grey;
        colors[3] = Color.black;
        colors[4] = Color.magenta;
        colors[5] = Color.gray;
        for (int i = 0; i < Blocks.Count; i++)
        {
            Blocks[i].transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material.color = colors[Random.Range(0, colors.Length)];
        }
    }
    void MakeBuilding()
    {
        Blocks.Clear();
        for (int i = 0; i < BlockCount; i++)
        {
            var BlockClone = Instantiate(m_Block, new Vector3(transform.position.x, transform.position.y + i * 0.11f, transform.position.z), Quaternion.identity);
            BlockClone.transform.SetParent(transform);
            Blocks.Add(BlockClone.transform);
        }
    }
    [Button]
    void DesBlock()
    {
        if (GameManager.Instance.m_Level.LvlChild() <= 1)
        {
            UIManager.Instance.WinScreen.SetActive(true);
            GameManager.Instance.IsStart = false;
            Destroy(GameManager.Instance.m_Level.gameObject);
            GameManager.Instance.lvlNo++;
        }
        if (Blocks.Count <= 1)
        {
            Destroy(gameObject);
           //GameManager.Instance.BricksList.Remove(this.gameObject);
            return; 
        }
        else
        {
            BlockCount -= 1;
            m_TextPower.text = BlockCount.ToString();
            FindTopBlock().gameObject.transform.GetChild(2).gameObject.SetActive(true);
            Destroy(FindDownBlock().gameObject);
            Blocks.Remove(FindDownBlock());
            foreach (var item in Blocks)
            {
                item.position = new Vector3(item.position.x, item.position.y - .11f, item.position.z);
            }
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            DesBlock();
        }  
        if(other.gameObject.layer == 11)
        {
            Debug.Log("You Loose");
            UIManager.Instance.LooseScreen.SetActive(true);
        }
    }
}
public enum ColorEnum
{
    GREEN, RED, YELLOW
}

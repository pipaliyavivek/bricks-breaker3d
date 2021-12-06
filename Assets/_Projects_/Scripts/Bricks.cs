using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bricks : MonoBehaviour
{
    [SerializeField] TextMeshPro m_TextPower;
    private int Power = 10;
    public ColorEnum m_BlockColor;

    [Button]
    void Start()
    {
        m_TextPower = transform.parent.GetComponentInChildren<TextMeshPro>();
        m_TextPower.text = Power.ToString();
    }    
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Power -= 1;
            m_TextPower.text = Power.ToString();
            if (Power < 1)
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    [Button]
    public void SetBlockColor()
    {
        switch (m_BlockColor)
        {
            case ColorEnum.GREEN:
                SetColor(Color.green);
                break;
            case ColorEnum.RED:
                SetColor(Color.red);
                break;
            case ColorEnum.YELLOW:
                SetColor(Color.yellow);
                break;
            default:
                SetColor(Color.white);
                break;
        }
    }
    void SetColor(Color i_Color)
    {
       GetComponent<SkinnedMeshRenderer>().material.color = i_Color;
    }

}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]Rigidbody m_Rigidbody;
    public float movespeed;

    public Transform mPoint;

    [Button]
    void SetRefs()
    {
        m_Rigidbody = GetComponent<Rigidbody>(); 
    }
    void Start()
    {
        movespeed = 8;
    }
    void OnDisable()
    {
        m_Rigidbody.velocity = Vector3.zero;
        m_Rigidbody.angularVelocity = Vector3.zero;
/*        transform.localEulerAngles = new Vector3(90, 90, 0);
        transform.localPosition = new Vector3(-5.82f, 1.86f, -1.74f);*/
        transform.localEulerAngles = Vector3.zero;
        transform.localPosition = Vector3.zero;     
    }
    void OnEnable()
    {
        m_Rigidbody.velocity = transform.forward * GameManager.Instance.mBallMoveSpeed; 
    }
    void FixedUpdate()
    {
        if (GameManager.Instance.mBallMoveSpeed > 0) 
        {
            //if (m_Rigidbody)m_Rigidbody.MovePosition(Vector3.forward * 0.08f * Time.deltaTime);
            //m_Rigidbody.MovePosition(m_Rigidbody.position + transform.forward * movespeed * Time.deltaTime);
            //m_Rigidbody.AddForce(transform.forward * 20, ForceMode.Acceleration);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.layer == 11)
        {
            gameObject.SetActive(false);
            transform.SetParent(GameManager.Instance.Point.transform.GetChild(0));
            transform.localPosition = Vector3.zero;
            //Destroy(gameObject);
        }
        /*if (other.gameObject.layer == 9)
        {
            var curDir = transform.TransformDirection(Vector3.forward * Random.Range(91f, 180f));
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(curDir, other.transform.position.x < 0 ? Vector3.forward : Vector3.back));
        }
        else
        {
            var curDir = transform.TransformDirection(Vector3.forward);
            transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(curDir, other.transform.position.x < 0 ? Vector3.left : Vector3.right));
        }*/
    }
    /*void OnTriggerEnter(Collider other)
    {
        var curDir = transform.TransformDirection(Vector3.forward);
        transform.rotation = Quaternion.FromToRotation(Vector3.forward, Vector3.Reflect(curDir, other.transform.position.x < 0 ? Vector3.left : Vector3.right));
    }*/ 
}

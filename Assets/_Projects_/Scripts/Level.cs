using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level : MonoBehaviour
{
    [SerializeField] Vector2 grid;
    public GameObject BrickPrefab;
    [SerializeField] public List<GameObject> BricksList = new List<GameObject>();
    private float Timer = 0;

    Tween MoveTween;

    void OnEnable()
    {
        MoveTween?.Kill(true);
        MoveTween = DOVirtual.Float(0, -10, 100f, callback);
    }
    void callback(float val)
    {
        if (!MoveTween.IsPlaying()) return;
        var pos = transform.localPosition;
        pos.z = val;
        transform.localPosition = pos;
    }
    void Update()
    {
        //Timer -= Time.deltaTime/5000;
       // transform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z + Timer);
    }
    [Button]
    public void BricksGrid()
    {
        BricksList.Clear();
        while (transform.childCount > 0)
        {
            foreach (Transform item in transform)
            {
                DestroyImmediate(item.gameObject);
            }
        }
        for (int i = 1; i <= grid.x; i++)
        {
            for (int j = 1; j <= grid.y; j++)
            {
                GameObject Bricks = Instantiate(BrickPrefab, new Vector3(transform.localPosition.x + i, transform.localPosition.y, transform.localPosition.z + j), Quaternion.identity) as GameObject;
                BricksList.Add(Bricks);
                Bricks.transform.parent = transform;
            }
        }
    }
    public int LvlChild()
    {
        return transform.childCount;    
    }
}

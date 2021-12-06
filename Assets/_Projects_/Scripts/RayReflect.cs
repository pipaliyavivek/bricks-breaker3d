
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class RayReflect : MonoBehaviour
{

    [Header("Maximum Distance")]
    public float maxDistance = 50;

    [Header("Maximum number of reflections")]
    public int maxReflectTimes = 10;

  
    LineRenderer lineRender;


    List<Vector3> linePosList;

    private void Awake()
    {
        lineRender = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        linePosList = new List<Vector3>();
        linePosList.Add(transform.position);//Start from yourself

        GetPoint(transform.position, transform.forward, maxDistance, maxReflectTimes);//Get the reflection point

        //Recursion ends and starts rendering rays
        lineRender.positionCount = linePosList.Count;
        lineRender.SetPositions(linePosList.ToArray());
    }
    public void GetPoint(Vector3 start, Vector3 dir, float dis, int times)
    {
        RaycastHit hit;
        if (Physics.Raycast(start, dir, out hit, dis))
        {
            linePosList.Add(hit.point);
            Vector3 reflectDir = Vector3.Reflect(dir, hit.normal);//Calculate the reflection angle, Unity comes with it, you can write a more efficient function instead
            float nextdis = dis - (hit.point - transform.position).magnitude;
            if (nextdis > 0 && times > 0)//recursively calculate all reflection points before the distance or times are consumed
                GetPoint(hit.point, reflectDir, nextdis, --times);
        }
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform[] pointList;
    public Transform lookAt;
    public float speed;

    private Vector3 endpos;
    private Vector3 startpos;
    private Vector3 nextpos;

    private float time = 0;

    private void Start()
    {
        endpos = pointList[0].position;
        startpos = pointList[1].position;
        transform.position = startpos;
    }

    void Update()
    {
        transform.LookAt(lookAt);

        time += Time.deltaTime;

        transform.position = Vector3.Lerp(startpos, endpos, time * speed);
        float x = transform.position.x;

        if (x == endpos.x)
        {
            endpos = endpos == pointList[0].position ? pointList[1].position : pointList[0].position;
            startpos = startpos == pointList[0].position ? pointList[1].position : pointList[0].position;

            time = 0;
        }
    }
}

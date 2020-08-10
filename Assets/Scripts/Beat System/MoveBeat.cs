using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBeat : MonoBehaviour
{
    private Vector3 targetLocation;
    private Vector3 spawnLocation;
    private Vector3 endLocation;

    public float distance;
    public float timeBetweenBeats;
    public float speed;

    void Start()
    {
        // targetLocation = Conductor.instance.targetLocation.position;
        // spawnLocation = Conductor.instance.spawnLocation.position;
        // endLocation = Conductor.instance.endLocation.position;

        // distance = Vector3.Distance(targetLocation, spawnLocation);
        // speed = distance/timeBetweenBeats;
    }
    void FixedUpdate()
    {
        // gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endLocation, speed * Time.deltaTime);
    }
}

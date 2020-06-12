using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.Windows.Speech;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour
{
    public GameObject target;
    public float updateRate = 2f;
    private Seeker seeker;
    private Rigidbody2D rigidbody;
    public float speed = 300f;
    public Path path;

    public ForceMode2D force;

    public bool pathIsEnd=false;
    public float nextWaypointDis = 3;
    private int currentWaypoint = 0;

    private bool searchingForPlayer = false;
    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        seeker = GetComponent<Seeker>();
        rigidbody = GetComponent<Rigidbody2D>();
        if (target==null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchPlayer());
            }
            return;
        }
        seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
        StartCoroutine(UpdatePath());
    }
    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    IEnumerator UpdatePath()
    {
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchPlayer());
            }
            yield break;
        }
        if (target!=null)
        {
            seeker.StartPath(transform.position, target.transform.position, OnPathComplete);
        }
        yield return new WaitForSeconds(1 / updateRate);
        StartCoroutine(UpdatePath());
    }
    private void FixedUpdate()
    {
        Debug.Log(target);
        if (target == null)
        {
            if (!searchingForPlayer)
            {
                searchingForPlayer = true;
                StartCoroutine(SearchPlayer());
            }
            return;
        }
        if (path == null)
            return;
        if (currentWaypoint>=path.vectorPath.Count)
        {
            if (pathIsEnd)
                return;
            pathIsEnd = true;
            return;
        }
        pathIsEnd = false;
        Vector3 dir = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        dir *= speed * Time.fixedDeltaTime;
        rigidbody.AddForce(dir, force);
        float dis = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
        if (dis<nextWaypointDis)
        {
            currentWaypoint++;
            return;
        }
    }
    IEnumerator SearchPlayer()
    {
        GameObject res = GameObject.FindGameObjectWithTag("Player");
        if (res == null)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(SearchPlayer());
        }       
        else
        {
            target = res;
            searchingForPlayer = false;
            StartCoroutine(UpdatePath());
            yield return false;
        }
    }
}

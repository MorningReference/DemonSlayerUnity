using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] bool isPatrol;
    WaveConfig waveConfig;
    List<Transform> waypoints;

    // [Range(0f, 5f)]
    // [SerializeField] float moveSpeed = 2f;
    int waypointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waypoints[waypointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPatrol == true)
        {
            Patrol();
        }
        else
        {
            Move();
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }
    private void Move()
    {

        if (waypointIndex <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIndex].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementThisFrame);

            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
            // else
            // {
            //     Destroy(gameObject);
            // }
        }
    }
    private void Patrol()
    {
        // just 2 points right?
        var pos0 = waypoints[0].transform.position;
        var pos1 = waypoints[1].transform.position;
        var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
        if (waypointIndex == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos1, movementThisFrame);
            transform.localScale = new Vector3(1f, 1f, 1f);

            if (transform.position.x <= pos1.x)
            {
                waypointIndex = 1;
            }
        }
        else if (waypointIndex == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, pos0, movementThisFrame);
            transform.localScale = new Vector3(-1f, 1f, 1f);

            if (transform.position.x >= pos0.x)
            {
                waypointIndex = 0;
            }

        }

    }
}


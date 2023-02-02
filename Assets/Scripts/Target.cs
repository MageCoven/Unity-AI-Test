using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

public class Target : MonoBehaviour
{
    [SerializeField] private float hitReward;
    [SerializeField] private Transform[] spawnPoints;

    void Awake()
    {
        this.Reset();
        Academy.Instance.OnEnvironmentReset += this.Reset;
    }

    void OnCollisionEnter(Collision collision)
    {
        PlayerAgent agent = collision.gameObject.GetComponent<PlayerAgent>();
        if (agent == null)
        {
            agent.AddReward(this.hitReward);
            this.Reset();
        }
    }

    private void Reset()
    {
        this.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
    }
}

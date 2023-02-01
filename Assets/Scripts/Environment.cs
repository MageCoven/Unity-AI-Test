using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Environment : MonoBehaviour
{
    [SerializeField] private GameObject agentPrefab;

    void Awake()
    {
        Debug.LogWarning("Environment script is not implemented yet.");

        if (this.agentPrefab == null)
        {
            // TODO: Change this to a better fitting exception, perhaps a custom one
            throw new UnityException("Environment missing an Agent Prefab to instantiate");
        }
    }

    // TODO: Instantiate agents according to number of agents
}

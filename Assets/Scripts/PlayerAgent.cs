using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

[RequireComponent(typeof(Rigidbody), typeof(RayPerceptionSensorComponent3D))]
public class PlayerAgent : Agent
{
    [SerializeField] private Transform spawn;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float movementSpeed;

    private new Rigidbody rigidbody;

    // Initialize the agent, happens only once.
    public override void Initialize()
    {
        this.rigidbody = this.GetComponent<Rigidbody>();
        if (this.rigidbody == null)
        {
            throw new UnityException("Agent requires a Rigidbody");
        }

        Academy.Instance.OnEnvironmentReset += this.Reset;
    }

    // Recieve actions that the AI wants to perform and perform them.
    public override void OnActionReceived(ActionBuffers actions)
    {
        ActionSegment<float> continousActions = actions.ContinuousActions;
        float forwardMovement = Mathf.Clamp(continousActions[0], -1f, 1f);
        float strafeMovement = Mathf.Clamp(continousActions[1], -1f, 1f);
        float rotation = Mathf.Clamp(continousActions[2], -1f, 1f);

        this.transform.Rotate(
            this.transform.up * rotation * Time.deltaTime * this.rotationSpeed);

        Vector3 movementDirection = this.transform.TransformDirection(
                new Vector3(forwardMovement, 0, strafeMovement));
        this.rigidbody.AddForce(movementDirection * this.movementSpeed);
    }

    // Add the velocity as an observation in addition to
    // RayPerceptionSensorComponent3D
    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(this.rigidbody.velocity);
    }

    // Shouldn't be needed since no manual control is assumed.
    // public override void Heuristic(in ActionBuffers actions) {}

    // Nothing is being done before the episode begins.
    // public override void OnEpisodeBegin() {}

    void OnCollisionEnter(Collision collision)
    {
        // TODO: Implement target hitting
    }

    private void Reset()
    {
        this.transform.position = this.spawn.position;
        this.transform.rotation = this.spawn.rotation;
        this.rigidbody.velocity = Vector3.zero;
    }
}

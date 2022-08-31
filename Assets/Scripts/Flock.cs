using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FlockBehavior
{
    public class Flock : MonoBehaviour
    {
        public LayerMask agentLayer;
        public LayerMask groundLayer;
        public FlockAgent agentPrefab;
        public FlockBehavior behavior;

        [Range(10, 500)]
        public int startingCount = 250;
        const float AgentDensity = 0.08f;

        [Range(1f, 100f)]
        public float driveFactor = 10f;
        [Range(1f, 100f)]
        public float maxSpeed = 5f;
        [Range(1f, 10f)]
        public float neighborRadius = 1.5f;
        [Range(0f, 1f)]
        public float avoidanceRadiusMultiplier = 0.5f;

        float squareMaxSpeed;
        float squareNeighborRadius;
        float squareAvoidanceRadius;
        List<FlockAgent> agents = new();

        public float SquareAvoidanceRadius => squareAvoidanceRadius;

        private void Start()
        {
            squareMaxSpeed = maxSpeed * maxSpeed;
            squareNeighborRadius = neighborRadius * neighborRadius;
            squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

            for (int i = 0; i < startingCount; i++)
            {
                FlockAgent newAgent = Instantiate(
                    agentPrefab,
                    RandomPosition(),
                    Quaternion.Euler(Vector3.up * Random.Range(0f, 360f)),
                    transform
                    );

                newAgent.name = $"Agent {i}";
                newAgent.Init(this);
                agents.Add(newAgent);
            }
        }

        private void Update()
        {
            foreach (FlockAgent agent in agents)
            {
                List<Transform> context = GetNearbyObjects(agent);
                Vector3 move = behavior.CalculateMove(agent, context, this);
                move *= driveFactor;
                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
                move.y = 0f;
                agent.Move(move);
            }
        }

        private Vector3 RandomPosition()
        {
            float radius = 5 * startingCount * AgentDensity;
            return new Vector3(
                Random.Range(-radius, radius),
                0f,
                Random.Range(-radius, radius)
                );
        }

        private List<Transform> GetNearbyObjects(FlockAgent agent)
        {
            List<Transform> context = new();
            Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
            foreach (Collider c in contextColliders)
            {
                if (c.gameObject.layer != Mathf.Log(groundLayer, 2) && c != agent.AgentCollider)
                {
                    context.Add(c.transform);
                }
            }

            return context;
        }
    }
}
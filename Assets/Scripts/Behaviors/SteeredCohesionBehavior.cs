using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FlockBehavior
{
    [CreateAssetMenu(menuName = "TooLoo/Flock/Behavior/SteeredCohesion")]
    public class SteeredCohesionBehavior : FilteredFlockBehavior
    {
        Vector3 currentVelocity;
        public float agentSmoothTime = 0.5f;

        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0) return Vector3.zero;

            // Add all points and average
            Vector3 cohesionMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                cohesionMove += item.position;
            }

            cohesionMove /= context.Count;

            // Create offset from agent position
            cohesionMove -= agent.transform.position;
            cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref currentVelocity, agentSmoothTime);
            return cohesionMove;
        }
    }
}
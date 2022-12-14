using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FlockBehavior
{
    [CreateAssetMenu(menuName = "TooLoo/Flock/Behavior/Alignment")]
    public class AlignmentBehavior : FilteredFlockBehavior
    {
        public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
        {
            if (context.Count == 0)
                return agent.transform.forward;

            // Add all points and average
            Vector3 alignmentMove = Vector3.zero;
            List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
            foreach (Transform item in filteredContext)
            {
                alignmentMove += item.transform.forward;
            }

            alignmentMove /= context.Count;
            return alignmentMove;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FlockBehavior
{
    [CreateAssetMenu(menuName = "TooLoo/Flock/Filter/Same Flock")]
    public class SameFlockFilter : ContextFilter
    {
        public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbors)
        {
            List<Transform> filtered = new();
            foreach (Transform neighbor in originalNeighbors)
            {
                FlockAgent itemAgent = neighbor.GetComponent<FlockAgent>();
                if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
                {
                    filtered.Add(neighbor);
                }
            }
            return filtered;
        }
    }
}
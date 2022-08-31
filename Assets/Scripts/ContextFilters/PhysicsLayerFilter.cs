using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FlockBehavior
{
    [CreateAssetMenu(menuName = "TooLoo/Flock/Filter/Physics Layer")]
    public class PhysicsLayerFilter : ContextFilter
    {
        public LayerMask mask;

        public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbors)
        {
            List<Transform> filtered = new();
            foreach (Transform neighbor in originalNeighbors)
            {
                if (mask == (mask | (1 << neighbor.gameObject.layer)))
                {
                    filtered.Add(neighbor);
                }
            }
            return filtered;
        }
    }
}
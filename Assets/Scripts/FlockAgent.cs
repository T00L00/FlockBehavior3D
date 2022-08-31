using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FlockBehavior
{
    [RequireComponent(typeof(Collider))]
    public class FlockAgent : MonoBehaviour
    {
        Flock agentFlock;
        Collider agentCollider;

        public Collider AgentCollider => agentCollider;
        public Flock AgentFlock => agentFlock;

        private void Start()
        {
            agentCollider = GetComponent<Collider>();
        }

        public void Init(Flock flock)
        {
            agentFlock = flock;
        }

        public void Move(Vector3 velocity)
        {
            transform.forward = velocity;
            transform.position += velocity * Time.deltaTime;
        }
    }
}
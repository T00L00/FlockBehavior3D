using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TooLoo.AI.FlockBehavior
{
    public abstract class FilteredFlockBehavior : FlockBehavior
    {
        public ContextFilter filter;
    }
}
using NodeCanvas.Framework;
using UnityEngine;

namespace NodeCanvas.Tasks.Conditions
{
    public class DistanceFromCT : ConditionTask
    {
        public BBParameter<float>     detectionRadius = 2f;
        public BBParameter<Transform> target;
        public BBParameter<float>     fieldOfView = 90f;
        public BBParameter<LayerMask> mask;
        public BBParameter<Transform> eye;

        public enum RangeMode { InsideRange, OutsideRange, OutOfLineOfSight }
        public RangeMode rangeMode = RangeMode.InsideRange;

        protected override string OnInit()
        {
            return null;
        }

        protected override bool OnCheck()
        {
            if (target.value == null || agent == null)
                return false;

            Vector3 eyePos = agent.transform.position; //get the eye position of the drone
            if (eye.value != null)
                eyePos = eye.value.position;

            Vector3 toTarget = target.value.position - eyePos; //direction and distance to target
            float flatDist   = new Vector3(toTarget.x, 0f, toTarget.z).magnitude; //flat distance to ignore the height difference of the drone and player
            float radius     = detectionRadius.value;

            // OutsideRange: player is beyond the detection radius
            if (rangeMode == RangeMode.OutsideRange)
                return flatDist > radius;

            // OutOfLineOfSight: player is within radius but raycast is blocked
            if (rangeMode == RangeMode.OutOfLineOfSight)
            {
                if (flatDist > radius) //if player is already outside "not visible"
                    return true;

                Vector3 rayDirection = (target.value.position - eyePos).normalized; //cast a ray to cehck if something blocks its view
                if (Physics.Raycast(eyePos, rayDirection, toTarget.magnitude, mask.value, QueryTriggerInteraction.Ignore)) //if something hits, line of sight is blocked
                    return true;

                return false; //else player is visible
            }

            // InsideRange: within radius and clear line of sight
            if (flatDist > radius)
                return false;

            Vector3 dir = (target.value.position - eyePos).normalized;
            if (Physics.Raycast(eyePos, dir, toTarget.magnitude, mask.value, QueryTriggerInteraction.Ignore))
                return false;

            return true;
        }
    }
}

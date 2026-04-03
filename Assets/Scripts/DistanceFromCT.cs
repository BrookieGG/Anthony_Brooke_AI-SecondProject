using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class DistanceFromCT : ConditionTask {

        public BBParameter<float> detectionRadius = 2f;
        public BBParameter<Transform> target;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            return null;
        }

        //Called whenever the condition gets enabled.
        protected override void OnEnable()
        {

        }

        //Called whenever the condition gets disabled.
        protected override void OnDisable()
        {

        }

        //Called once per frame while the condition is active.
        //Return whether the condition is success or failure.
        protected override bool OnCheck()
        {
            if (target.value == null || agent == null)
            {
                return false;
            }

            Vector3 agentPos = agent.transform.position;
            Vector3 targetPos = target.value.position;

            agentPos.y = 0f;
            targetPos.y = 0f;

            float sqrDistance = (agentPos - targetPos).sqrMagnitude;
            float sqrRadius = detectionRadius.value * detectionRadius.value;

            return sqrDistance <= sqrRadius;
        }
    }
}
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class DistanceFromCT : ConditionTask {

        public BBParameter<float> detectionRadius = 2f;
        public BBParameter<Transform> target;
        public BBParameter<float> fieldOfView = 90f;
        public BBParameter<LayerMask> mask;
        public BBParameter<Transform> eye;

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

            Vector3 targetPos = target.value.position;
            Vector3 eyePos = eye.value.position;
            Vector3 toTarget = targetPos - eyePos;

            float flatDist = new Vector3(toTarget.x, 0f, toTarget.z).magnitude;
            float radius = detectionRadius.value;

            if (flatDist > radius)
            {
                return false;
            }

            Vector3 rayOrigin = eyePos;
            Vector3 rayDirection = (targetPos - rayOrigin).normalized;
            float rayDist = toTarget.magnitude;

            if (Physics.Raycast(rayOrigin, rayDirection, rayDist, mask.value,QueryTriggerInteraction.Ignore))
            {
                return false;
            }
            return true;
        }
    }
}
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class DodgeAT : ActionTask {

        public BBParameter<float> dodgeSpeed = 6f;
        public BBParameter<float> dodgeDistance = 3f;
        public BBParameter<Transform> targetTransform;
        public BBParameter<float> sampleMaxDist = 2f;
        public BBParameter<float> arrivalThreshold = 0.25f;

        private NavMeshAgent navAgent;
        private Vector3 destination;
        private float originalSpeed;
        private bool destinationSet;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
            {
                return "DodgeAT requires a NavMeshAgent.";
            }

            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            if (targetTransform.value == null)
            {
                EndAction(false);
                return;
            }

            originalSpeed = navAgent.speed;
            navAgent.speed = dodgeSpeed.value;

            destinationSet = TrySetDodgeDestination();

            if (!destinationSet)
            {
                navAgent.speed = originalSpeed;
                EndAction(false);
            }
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            if (!destinationSet)
            {
                EndAction(false);
                return;
            }

            if (navAgent.pathPending)
            {
                return;
            }

            if (navAgent.remainingDistance <= arrivalThreshold.value)
            {
                EndAction(true);
            }
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {
            if (navAgent != null)
            {
                navAgent.speed = originalSpeed;
            }
        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }

        private bool TrySetDodgeDestination()
        {
            Vector3 awayFromTarget = (agent.transform.position - targetTransform.value.position).normalized;

            if (awayFromTarget.sqrMagnitude < 0.0001f)
            {
                awayFromTarget = -agent.transform.forward;
            }

            for (int i = 0; i < 20; i++)
            {
                Vector2 random2D = Random.insideUnitCircle.normalized;

                Vector3 randomOffset = new Vector3(random2D.x, 0f, random2D.y);

                // Bias the result away from the target
                Vector3 dodgeDirection = (awayFromTarget + randomOffset * 0.75f).normalized;

                Vector3 candidate = agent.transform.position + dodgeDirection * dodgeDistance.value;

                if (NavMesh.SamplePosition(candidate, out NavMeshHit hit, sampleMaxDist.value, NavMesh.AllAreas))
                {
                    destination = hit.position;
                    navAgent.SetDestination(destination);
                    return true;
                }
            }

            return false;
        }
    }
}
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class DodgeAT : ActionTask {

        public BBParameter<float> currentSpeed;
        public BBParameter<float> dodgeSpeed;
        public float dodgeDistance;
        public BBParameter<Transform> targetTransform;
        public BBParameter<float> sampleMaxDist = 4f;
        private NavMeshAgent navAgent;
        private Vector3 destination;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();

            return null;
        }

        //This is called once each time the task is enabled.
        //Call EndAction() to mark the action as finished, either in success or failure.
        //EndAction can be called from anywhere.
        protected override void OnExecute()
        {
            //Move the object towards the target Transform

            Vector3 directionToTarget = targetTransform.value.position - agent.transform.position;

            if (directionToTarget.sqrMagnitude < 0.0001f)
            {
                EndAction(true);
                return;
            }

            //agent.transform.position += (directionToTarget.normalized * -1) * (currentSpeed.value * dodgeMultiplier) * Time.deltaTime;

            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget);
            agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, targetRotation, 720f * Time.deltaTime);

            float distanceToTarget = directionToTarget.magnitude;

            float baseSpeed = navAgent.speed;

            navAgent.speed = dodgeSpeed.value;
            SetDestination();

            if (agent.transform.localPosition.x - destination.x < 0.1)
            {
                EndAction(true);
            }
        }

        //Called once per frame while the action is active.
        protected override void OnUpdate()
        {
            
        }

        //Called when the task is disabled.
        protected override void OnStop()
        {

        }

        //Called when the task is paused.
        protected override void OnPause()
        {

        }
        private void SetDestination()
        {
            Vector3 circleCenter = agent.transform.position + agent.transform.forward * dodgeDistance;
            Vector3 randomPoint = Random.insideUnitCircle.normalized * dodgeDistance;
            destination = circleCenter + new Vector3(randomPoint.x, agent.transform.position.y, randomPoint.y);

            NavMeshHit hit;

            for (int i = 0; i < 20; i++)
            {
                Vector3 randomOffset = Random.insideUnitCircle * dodgeDistance;

                if (NavMesh.SamplePosition(destination, out hit, sampleMaxDist.value, NavMesh.AllAreas))
                {
                    navAgent.SetDestination(hit.position);
                }
            }
        }
    }
}
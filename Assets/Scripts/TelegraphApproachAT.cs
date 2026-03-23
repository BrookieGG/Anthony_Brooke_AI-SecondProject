using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class TelegraphApproachAT : ActionTask {

        public BBParameter<float> speed;
        public BBParameter<Transform> targetTransform;

        public BBParameter<float> rotationSpeed;
        public float moveAngle = 20f;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			//EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
            if (targetTransform.value == null)
            {
                EndAction(false);
                return;
            }
            
            //Move the object towards the target Transform

            Vector3 directionToMove = targetTransform.value.position - agent.transform.position;

            if (directionToMove.sqrMagnitude < 0.0001f) //stop if close to target
            {
                EndAction(true);
                return;
            }

            directionToMove.y = 0f;

            //rotate
            Quaternion targetRotation = Quaternion.LookRotation(directionToMove);
            agent.transform.rotation = Quaternion.RotateTowards(agent.transform.rotation, targetRotation, rotationSpeed.value * Time.deltaTime);

            //only move if facing the target
            float angle = Vector3.Angle(agent.transform.forward, directionToMove);
            if (angle < moveAngle)
            {
                agent.transform.position += directionToMove.normalized * speed.value * Time.deltaTime;
            }

            float distanceToTarget = directionToMove.magnitude;

            if (distanceToTarget < 0.5f)
            {
                EndAction(true);
            }
        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}
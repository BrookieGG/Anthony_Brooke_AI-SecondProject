using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions
{

	public class WanderAT : ActionTask
	{

		public float wanderDist = 6f;
		public float wanderRadius = 5f;
		//public BBParameter<float> wanderDist = 10f;
		//public BBParameter<float> wanderRadius = 5f;
		public BBParameter<float> sampleMaxDist = 4f;

		private NavMeshAgent navAgent;

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
			//SetDestination();
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate()
		{

			//wander code I had before the inclass demo
			//if (navAgent.remainingDistance <= navAgent.stoppingDistance)
			//{
			//	Vector3 origin = agent.transform.localPosition;
			//	Vector3 facingDirection = agent.transform.forward;

			//	for (int i = 0; i < 20; i++)
			//	{
			//		Vector3 randomOffset = Random.insideUnitCircle * wanderRadius.value;

			//		Vector3 destination = origin + facingDirection + randomOffset;

			//		if (NavMesh.SamplePosition(destination, out NavMeshHit hit, sampleMaxDist.value, NavMesh.AllAreas))
			//		{
			//			navAgent.SetDestination(destination);
			//			return;
			//		}
			//	}
			//}

			if (navAgent.remainingDistance < 0.25f && !navAgent.pathPending)
			{
                SetDestination();
            }
		}

		//Called when the task is disabled.
		protected override void OnStop()
		{

		}

		//Called when the task is paused.
		protected override void OnPause()
		{

		}

		private void VisualizeWander(Vector3 currentCircleCenter, Vector3 currentDestination, float pathUpdateFrequency)
		{
			Debug.DrawLine(agent.transform.position, currentCircleCenter, Color.red, pathUpdateFrequency);
			for (int i = 0; i < 360; i += 12)
			{
				Vector3 p1 = new Vector3(Mathf.Cos(i * Mathf.Deg2Rad), 0f, Mathf.Sin(i * Mathf.Deg2Rad)) * wanderRadius;
				Vector3 p2 = new Vector3(Mathf.Cos((i + 12) * Mathf.Deg2Rad), 0f, Mathf.Sin((i + 12) * Mathf.Deg2Rad)) * wanderRadius;

				Debug.DrawLine(currentCircleCenter + p1, currentCircleCenter + p2, Color.cyan, pathUpdateFrequency);
			}

			Debug.DrawLine(agent.transform.position, currentDestination, Color.magenta, pathUpdateFrequency);
		}

	

	private void SetDestination()
		{
			Vector3 circleCenter = agent.transform.position + agent.transform.forward * wanderDist;
			Vector3 randomPoint = Random.insideUnitCircle.normalized * wanderRadius;
			Vector3 destination = circleCenter + new Vector3(randomPoint.x, agent.transform.position.y, randomPoint.y);

			VisualizeWander(circleCenter, destination, 5f);

			NavMeshHit hit;

            for (int i = 0; i < 20; i++)
			{
                Vector3 randomOffset = Random.insideUnitCircle * wanderRadius;

                if (NavMesh.SamplePosition(destination, out hit, sampleMaxDist.value, NavMesh.AllAreas))
                {
                    navAgent.SetDestination(hit.position);
                }
            }
		}
	}
}
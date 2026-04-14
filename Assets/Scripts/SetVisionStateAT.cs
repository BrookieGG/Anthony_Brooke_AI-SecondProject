using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Actions {

	public class SetVisionStateAT : ActionTask {
		public enum VisionState {patrol, alert, searching}
		public VisionState visionState = VisionState.patrol;

		private DroneVision droneVision;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			droneVision = agent.GetComponent<DroneVision>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			if (visionState == VisionState.patrol)
			{
				droneVision.SetPatrol();
			}
			else if (visionState == VisionState.alert)
			{
				droneVision.SetAlerted();
			}
            else if(visionState == VisionState.searching)
			{
				droneVision.SetSearching();
			}

            EndAction(true);
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}
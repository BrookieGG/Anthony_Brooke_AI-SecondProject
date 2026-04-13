using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class SetValueAT : ActionTask {

		public BBParameter<GameObject> otherObject; //references a gameobject with the blackboard on it
		public string varName; //name of the variable that will be modified
		public float newValue; //new value that will be assigned 
        


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

            Blackboard otherBlackboard = otherObject.value.GetComponent<Blackboard>(); //gets blackboard
            otherBlackboard.SetVariableValue(varName, newValue); //sets the variable in the blackboard to the new value
            Debug.Log(otherBlackboard.GetVariableValue<float>(varName)); //debug log test to see if it is set to the new value

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
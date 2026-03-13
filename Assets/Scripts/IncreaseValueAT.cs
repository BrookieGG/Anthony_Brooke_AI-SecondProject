using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class IncreaseValueAT : ActionTask {

        //private float currentValue;
        public BBParameter<GameObject> otherObject;
        public float rateOfChange;
        public BBParameter<float> variable;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
            if (otherObject == null)
            {
                otherObject = agent.gameObject;
            }
            return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
            
        }

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
          
            Blackboard otherBlackboard = otherObject.value.GetComponent<Blackboard>(); //get the blackboard
            float currentValue = otherBlackboard.GetVariableValue<float>(variable.name); //get the current value

            currentValue += rateOfChange * Time.deltaTime; //increase the value over time

            otherBlackboard.SetVariableValue(variable.name, currentValue); //set the variable

            Debug.Log(currentValue);

        }

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}
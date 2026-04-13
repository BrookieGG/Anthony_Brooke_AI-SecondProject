using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class IfValueCT : ConditionTask {

        public BBParameter<float> currentValue;
		public BBParameter<GameObject> otherobject;
        public float threshold;
		public enum Mode { GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual, Equal }
		public Mode mode;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
            Blackboard otherBlackboard = otherobject.value.GetComponent<Blackboard>(); //gets blackboard
			currentValue = otherBlackboard.GetVariableValue(varName, newValue); //sets the variable in the blackboard to the new value
            return null;
		}

		//Called whenever the condition gets enabled.
		protected override void OnEnable() {
			
		}

		//Called whenever the condition gets disabled.
		protected override void OnDisable() {
			
		}

		//Called once per frame while the condition is active.
		//Return whether the condition is success or failure.
		protected override bool OnCheck() {
            bool result = false;
			if (mode == Mode.Equal)
			{
				result = currentValue.value == threshold;
			}
			else if (mode == Mode.GreaterThan)
			{
				result = currentValue.value > threshold;
			}
			else if (mode == Mode.LessThan)
			{
				result = currentValue.value < threshold;
			}
			else if (mode == Mode.GreaterThanOrEqual)
			{
				result = currentValue.value >= threshold;
			}
			else
			{
				result = currentValue.value <= threshold;
			}

			return result;
		}
	}
}
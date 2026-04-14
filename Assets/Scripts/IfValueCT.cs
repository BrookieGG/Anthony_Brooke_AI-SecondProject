using NodeCanvas.Framework;
using ParadoxNotion.Design;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class IfValueCT : ConditionTask {

        public string variableName;
		public BBParameter<GameObject> otherobject;
        public float threshold;
		public enum Mode { GreaterThan, GreaterThanOrEqual, LessThan, LessThanOrEqual, Equal }
		public Mode mode;

		private Blackboard otherBlackboard;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
            otherBlackboard = otherobject.value.GetComponent<Blackboard>(); //gets blackboard
			
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
			float currentValue = otherBlackboard.GetVariable<float>(variableName).value;
			if (mode == Mode.Equal)
			{
				result = currentValue == threshold;
			}
			else if (mode == Mode.GreaterThan)
			{
				result = currentValue > threshold;
			}
			else if (mode == Mode.LessThan)
			{
				result = currentValue < threshold;
			}
			else if (mode == Mode.GreaterThanOrEqual)
			{
				result = currentValue >= threshold;
			}
			else
			{
				result = currentValue <= threshold;
			}

			return result;
		}
	}
}
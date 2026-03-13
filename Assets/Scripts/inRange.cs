using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Conditions {

	public class inRange : ConditionTask {

		public float Range = 2;
		public Transform object1;
		public Transform object2;
        public BBParameter<bool> range;


        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit(){
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

			float currentDistance = Vector3.Distance(object1.position, object2.position);
			if (currentDistance < Range) 
			{
                range.value = true;
            }
			else
			{
				range.value = false;
			}
			
			return range.value;
		}
	}
}
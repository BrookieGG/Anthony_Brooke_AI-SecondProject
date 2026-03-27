using NodeCanvas.Framework;
using ParadoxNotion.Design;


namespace NodeCanvas.Tasks.Conditions {

	public class ModuloCT : ConditionTask {

		public BBParameter<float> value;
		public BBParameter<float> modulo;
		public BBParameter<float> result;

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

			if (value.value % modulo.value == result.value)
			{
                return true;
            }
			else
			{
                return false;
            }
		}
	}
}
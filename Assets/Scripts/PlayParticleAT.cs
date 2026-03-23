using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;


namespace NodeCanvas.Tasks.Actions {

	public class PlayParticleAT : ActionTask {

		public BBParameter<ParticleSystem> particleSystem;
		public BBParameter<float> duration = 1f;

		private ParticleSystem system;
		private float timer;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			if (particleSystem.value == null)  //check if particle system is assigned
				{
                    EndAction(false); //fail
					return;
                }

			//spawn the particle system on the agent's position
			system = GameObject.Instantiate(particleSystem.value, agent.transform.position, Quaternion.identity);
			system.transform.SetParent(agent.transform); //attatch it to the agent so it moves with it

			timer = 0f;
			system.Play(); //start the particle system
		}

		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			if (system == null)
			{
				EndAction(false);
				return;
			}
			timer += Time.deltaTime;

			if (timer >= duration.value) //end action after timer
			{
				system.Stop();
				GameObject.Destroy(system.gameObject);
				EndAction(true); //success
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
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


namespace NodeCanvas.Tasks.Actions {

	public class FindClosestTileAT : ActionTask {

        public BBParameter<List<GameObject>> allTiles;
		public BBParameter<GameObject> closestTile;
		private float closestTileDistance;
		private GameObject closestTileObject;

        //Use for initialization. This is called only once in the lifetime of the task.
        //Return null if init was successfull. Return an error string otherwise
        protected override string OnInit() {
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {

            foreach (GameObject tile in allTiles.value) //loop through all potential targets
            {
                float distance = Vector3.Distance(agent.transform.position, tile.transform.position); //get the distance between the agent and the target

                if (distance < closestTileDistance) //check if target is within the set radius
                {
                    closestTileObject = tile;
                    closestTileDistance = distance;
                }

            }
            Blackboard otherBlackboard = agent.GetComponent<Blackboard>(); //gets blackboard
            otherBlackboard.SetVariableValue(closestTile.name, closestTileObject); //sets the variable in the blackboard to the new value

			if (closestTile.value != null)
			{
                EndAction(true);
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
using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions
{
    public class SetLightStateAT : ActionTask
    {
        public enum LightState { patrol, alert, searching }
        public LightState lightState = LightState.patrol;

        public Renderer targetRenderer;
        public Material patrolMaterial;
        public Material alertMaterial;
        public Material searchingMaterial;

        protected override void OnExecute()
        {
            if (targetRenderer == null)
            {
                targetRenderer = agent.GetComponent<Renderer>();
            }

            if (targetRenderer == null)
            {
                EndAction(false);
                return;
            }

            if (lightState == LightState.patrol && patrolMaterial != null)
            {
                targetRenderer.material = patrolMaterial;
            }
            else if (lightState == LightState.alert && alertMaterial != null)
            {
                targetRenderer.material = alertMaterial;
            }
            else if (lightState == LightState.searching && searchingMaterial != null)
            {
                targetRenderer.material = searchingMaterial;
            }
            else
            {
                EndAction(false);
                return;
            }

            EndAction(true);
        }
    }
}
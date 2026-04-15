using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;
using UnityEngine.AI;

namespace NodeCanvas.Tasks.Actions
{
    [Category("Custom")]
    public class ChargeAT : ActionTask
    {
        public BBParameter<Transform> targetTransform;

        public float rotationSpeed = 720f;
        public float windupDuration = 0.75f;

        public float speedMultiplier = 3f;
        public float chargeDuration = 0.75f;

        private NavMeshAgent navAgent;

        private float originalSpeed;
        private float windupTimer;
        private float chargeTimer;

        private enum State
        {
            Telegraph,
            Charge
        }

        private State currentState;

        protected override string OnInit()
        {
            navAgent = agent.GetComponent<NavMeshAgent>();

            if (navAgent == null)
                return "ChargeAT requires a NavMeshAgent."; //missing navmesh

            return null;
        }

        protected override void OnExecute()
        {
            if (targetTransform.value == null) //fail if no target
            {
                EndAction(false);
                return;
            }

            // HARD STOP immediately
            navAgent.isStopped = true;
            navAgent.ResetPath();

            originalSpeed = navAgent.speed; //store original speed

            windupTimer = windupDuration; //timers
            chargeTimer = chargeDuration;

            currentState = State.Telegraph; //start in telegraph
        }

        protected override void OnUpdate()
        {
            if (targetTransform.value == null)
            {
                StopCharge();
                EndAction(false);
                return;
            }

            switch (currentState) //switch between the two states/enums
            {
                case State.Telegraph:
                    UpdateTelegraph();
                    break;

                case State.Charge:
                    UpdateCharge();
                    break;
            }
        }

        private void UpdateTelegraph()
        {
            Vector3 targetPos = targetTransform.value.position;
            targetPos.y = agent.transform.position.y;

            Vector3 toTarget = targetPos - agent.transform.position;

            if (toTarget.sqrMagnitude > 0.001f) //rotate toward player/target
            {
                Quaternion targetRotation = Quaternion.LookRotation(toTarget.normalized);
                agent.transform.rotation = Quaternion.RotateTowards(
                    agent.transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            windupTimer -= Time.deltaTime; //countdown timer

            if (windupTimer <= 0f) //once timer done start charging
            {
                StartCharge();
            }
        }

        private void StartCharge()
        {
            navAgent.speed = originalSpeed * speedMultiplier; //increase speed
            navAgent.isStopped = false;

            currentState = State.Charge;
        }

        private void UpdateCharge()
        {
            //constantly update destination to simulate aggressive chase
            navAgent.SetDestination(targetTransform.value.position);

            chargeTimer -= Time.deltaTime; //countdown charge

            if (chargeTimer <= 0f) //stop charging when timer done
            {
                StopCharge();
                EndAction(true);
            }
        }

        private void StopCharge() //reset movement
        {
            navAgent.speed = originalSpeed;
            navAgent.isStopped = false;
        }

        protected override void OnStop()
        {
            StopCharge();
        }

        protected override void OnPause()
        {
            StopCharge();
        }
    }
}
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
                return "ChargeAT requires a NavMeshAgent.";

            return null;
        }

        protected override void OnExecute()
        {
            if (targetTransform.value == null)
            {
                EndAction(false);
                return;
            }

            // HARD STOP immediately (this fixes your "finishes patrol first" issue)
            navAgent.isStopped = true;
            navAgent.ResetPath();

            originalSpeed = navAgent.speed;

            windupTimer = windupDuration;
            chargeTimer = chargeDuration;

            currentState = State.Telegraph;
        }

        protected override void OnUpdate()
        {
            if (targetTransform.value == null)
            {
                StopCharge();
                EndAction(false);
                return;
            }

            switch (currentState)
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

            if (toTarget.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(toTarget.normalized);
                agent.transform.rotation = Quaternion.RotateTowards(
                    agent.transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }

            windupTimer -= Time.deltaTime;

            if (windupTimer <= 0f)
            {
                StartCharge();
            }
        }

        private void StartCharge()
        {
            navAgent.speed = originalSpeed * speedMultiplier;
            navAgent.isStopped = false;

            currentState = State.Charge;
        }

        private void UpdateCharge()
        {
            // IMPORTANT: constantly update destination to simulate aggressive chase
            navAgent.SetDestination(targetTransform.value.position);

            chargeTimer -= Time.deltaTime;

            if (chargeTimer <= 0f)
            {
                StopCharge();
                EndAction(true);
            }
        }

        private void StopCharge()
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
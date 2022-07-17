using UnityEngine;
using UnityEngine.AI;
using RPG.Core;

namespace RPG.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class Mover : MonoBehaviour, IAction
    {
        [SerializeField] Transform target;

        private NavMeshAgent navMeshAgent;
        private Animator animator;
        private ActionScheduler actionScheduler;
        private Health health;

        void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();
            actionScheduler = GetComponent<ActionScheduler>();
            health = GetComponent<Health>();
        }

        void Update()
        {
            navMeshAgent.enabled = !health.IsDead();
            UpdateAnimator();
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = localVelocity.z;
            animator.SetFloat("forwardSpeed", speed);
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public void StartMoveAction(Vector3 destination)
        {
            actionScheduler.StartAction(this);
            MoveTo(destination);
        }
    }
}

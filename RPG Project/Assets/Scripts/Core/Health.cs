using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        Animator animator;
        private bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            animator.SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
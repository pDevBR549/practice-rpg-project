using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;
using GameDevTV.Utils;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 70f;

        LazyValue<float> healthPoints;

        Animator animator;
        bool isDead = false;
        BaseStats baseStats = null;

        public float HealthPoints { get { return healthPoints.value; } }
        public float MaxHealthPoints { get { return baseStats.GetStat(Stat.Health); } }

        public bool IsDead()
        {
            return isDead;
        }

        private void Awake()
        {
            animator = GetComponent<Animator>();
            baseStats = GetComponent<BaseStats>();
            healthPoints = new LazyValue<float>(GetInitialHealth);
        }

        private float GetInitialHealth()
        {
            return baseStats.GetStat(Stat.Health);
        }

        private void OnEnable()
        {
            baseStats.OnLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            baseStats.OnLevelUp -= RegenerateHealth;
        }

        private void Start()
        {
            healthPoints.ForceInit();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            print(string.Format("{0} took damage: {1}", gameObject.name, damage.ToString()));
            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);
            
            if (healthPoints.value == 0)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;

            experience.GainExperience(baseStats.GetStat(Stat.ExperienceReward));
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            animator.SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value == 0)
            {
                isDead = true;
            }
        }

        public float GetPercentage()
        {
            return 100 * healthPoints.value / baseStats.GetStat(Stat.Health);
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = baseStats.GetStat(Stat.Health) * regenerationPercentage / 100;
            healthPoints.value = MathF.Max(healthPoints.value, regenHealthPoints);
        }
    }
}
using System;
using UnityEngine;
using TMPro;
using RPG.Attributes;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;
        TMP_Text healthHUD = null;

        private void Awake()
        {
            healthHUD = GetComponent<TMP_Text>();
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        void Update()
        {
            Health health = fighter.GetTarget();
            string healthText = health != null ? String.Format("{0:0}/{1:0}", health.HealthPoints, health.MaxHealthPoints) : "N/A";
            healthHUD.text = healthText;
        }
    }
}
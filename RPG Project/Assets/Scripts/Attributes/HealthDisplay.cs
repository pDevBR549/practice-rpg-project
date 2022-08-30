using System;
using UnityEngine;
using TMPro;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health = null;
        TMP_Text healthHUD = null;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthHUD = GetComponent<TMP_Text>();
        }

        void Update()
        {
            healthHUD.text = String.Format("{0:0}/{1:0}",  health.HealthPoints, health.MaxHealthPoints);
        }
    }
}
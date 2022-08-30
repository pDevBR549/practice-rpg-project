using System;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience experience = null;
        TMP_Text experienceHUD = null;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
            experienceHUD = GetComponent<TMP_Text>();
        }

        void Update()
        {
            experienceHUD.text = String.Format("{0:0}", experience.XP.ToString());
        }
    }
}
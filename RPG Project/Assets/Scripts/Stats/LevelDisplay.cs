using System;
using UnityEngine;
using TMPro;

namespace RPG.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats baseStats = null;
        TMP_Text levelHUD = null;

        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
            levelHUD = GetComponent<TMP_Text>();
        }

        void Update()
        {
            levelHUD.text = String.Format("{0:0}", baseStats.GetLevel().ToString());
        }
    }
}
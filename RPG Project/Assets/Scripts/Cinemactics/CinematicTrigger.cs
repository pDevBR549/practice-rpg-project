using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            print("is triggered");

            if (!alreadyTriggered && other.CompareTag("Player"))
            {
                print("is triggered 2");
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}
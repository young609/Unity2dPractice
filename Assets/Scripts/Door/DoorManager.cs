using System;
using UnityEngine;

namespace Door
{
    public class DoorManager : MonoBehaviour
    { 
        private static readonly int openHash = Animator.StringToHash("Open");
        private static readonly int openSpeedHash = Animator.StringToHash("OpenSpeed");
        
        private Animator animator;

        private int number;

        private void Start()
        {
            animator = GetComponent<Animator>();
            
            animator.SetFloat(openSpeedHash, 0f);
            animator.Play(openHash);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (number == 0)
            {
                PlayDoorAnimation(true);
            }
            
            number++;
        }

        private void OnTriggerExit(Collider other)
        {
            number--;

            if (number == 0)
            {
                PlayDoorAnimation(false);
            }
        }

        private void PlayDoorAnimation(bool isOpen)
        {
            var playSpeed = isOpen ? 1f : -1f;
            animator.SetFloat(openSpeedHash, playSpeed);
            var progress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            var clampedProgress = Mathf.Clamp01(progress);
            animator.Play(openHash, 0, clampedProgress);
        }

        private void OnCollisionEnter(Collision other)
        {
            Debug.Log(gameObject.name + " collided with " + other.gameObject.name);
        }
    }
}

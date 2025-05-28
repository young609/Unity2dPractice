using System;
using UnityEngine;

namespace Cat
{
    public class LevelScrollManager : MonoBehaviour
    {
        [SerializeField]
        private CatController catController;
        
        [SerializeField]
        private float scrollSpeedMultiplier;

        private void Start()
        {
            catController.OnMove += moveDistance =>
            {
                transform.position += Vector3.left * (moveDistance * scrollSpeedMultiplier);
            };
        }
    }
}

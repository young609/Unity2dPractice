using System;
using UnityEngine;

namespace Cat
{
    public class InfinityMapByMat : MonoBehaviour
    {
        [SerializeField]
        private float moveSpeed;
        
        [SerializeField]
        private float scrollSpeedMultiplier;

        [SerializeField]
        private bool isMove;
        
        [SerializeField]
        private bool isLeft;

        private Material material;
        
        private void Start()
        {
            material = GetComponent<Renderer>().material;
        }

        private void Update()
        {
            if (!isMove || !material)
            {
                return;
            }
            
            // 오프셋 이동
            var moveDirection = isLeft ? Vector2.left : Vector2.right;
            var moveVector = moveDirection * (moveSpeed * scrollSpeedMultiplier * Time.deltaTime);
            material.mainTextureOffset += moveVector;
        }
    }
}

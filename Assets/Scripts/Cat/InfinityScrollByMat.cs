using UnityEngine;

namespace Cat
{
    public class InfinityScrollByMat : MonoBehaviour
    {
        [SerializeField]
        private CatController catController;
        
        [SerializeField]
        private float scrollSpeedMultiplier = 1f;
        
        [SerializeField]
        private float pixelsPerUnit = 24f;
        
        private Material material;

        // texture 1 offset = unitWidth unity meter
        private float unitPerOffset;
        
        private void Start()
        {
            material = GetComponent<Renderer>().material;
            unitPerOffset = material.mainTexture.width / pixelsPerUnit;
            catController.OnMove += moveDistance =>
            {
                var deltaOffset = UnitToOffset(moveDistance);
                material.mainTextureOffset += Vector2.right * (deltaOffset * scrollSpeedMultiplier);
            };
        }

        private float UnitToOffset(float unit)
        {
            return unit / unitPerOffset;
        }
    }
}

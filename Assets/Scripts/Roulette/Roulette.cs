using UnityEngine;

namespace Roulette
{
    public class Roulette : MonoBehaviour
    {
        [SerializeField]
        private Transform roulettePageTf;

        [SerializeField]
        private float rouletteStartSpeed;
    
        [SerializeField]
        private float durationTime;
        
        [SerializeField]
        private AnimationCurve rouletteCurve;

        private float currentTime;
        
        public void StartRoulette()
        {
            currentTime = 0f;
        }

        private void Start()
        {
            currentTime = float.MaxValue;
        }

        private void Update()
        {
            if (currentTime < durationTime)
            {
                var rotateSpeed = rouletteCurve.Evaluate(currentTime/durationTime) * rouletteStartSpeed;
                var rotateAngle = rotateSpeed * Time.deltaTime;
                roulettePageTf.Rotate(Vector3.back, rotateAngle);
                currentTime += Time.deltaTime;
            }
        }
    }
}

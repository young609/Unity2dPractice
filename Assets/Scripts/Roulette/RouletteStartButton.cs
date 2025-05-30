using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Roulette
{
    public class RouletteStartButton : MonoBehaviour
    {
        [SerializeField]
        private Roulette roulette;
    
        [SerializeField]
        private Button rouletteButton;
    
        void Start()
        {
            rouletteButton.onClick.AddListener(() =>
            {
                roulette.StartRoulette();
            });
        }
    }
}

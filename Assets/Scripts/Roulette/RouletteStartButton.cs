using UnityEngine;
using UnityEngine.UI;

namespace Roulette
{
    public class RouletteStartButton : MonoBehaviour
    {
        [SerializeField]
        private RouletteManager rouletteManager;
    
        [SerializeField]
        private Button rouletteButton;
    
        void Start()
        {
            rouletteButton.onClick.AddListener(() =>
            {
                rouletteManager.StartRoulette();
            });
        }
    }
}

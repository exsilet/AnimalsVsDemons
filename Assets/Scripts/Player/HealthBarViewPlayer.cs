using TMPro;
using UnityEngine;

namespace Player
{
    public class HealthBarViewPlayer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _barText;
        
        public void OnValueTextChanged(float value)
        {
            _barText.text = $"{(int)value}";
        }
    }
}
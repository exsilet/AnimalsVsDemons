using UnityEngine;
using UnityEngine.UI;

namespace Enemy
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] private Image _hpImage;

        public void SetValue(float current, float max)
            => _hpImage.fillAmount = current / max;
    }
}
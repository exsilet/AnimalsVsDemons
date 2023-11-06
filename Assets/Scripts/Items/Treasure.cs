using Infrastructure.StaticData.Items;
using TMPro;
using UnityEngine;

namespace Items
{
    public class Treasure : MonoBehaviour
    {
        [SerializeField] private ItemData _itemData;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private GameObject _description;

        public ItemData ItemData => _itemData;

        private void OnMouseEnter()
            => _description.SetActive(true);

        private void OnMouseExit()
            => _description.SetActive(false);
    }
}

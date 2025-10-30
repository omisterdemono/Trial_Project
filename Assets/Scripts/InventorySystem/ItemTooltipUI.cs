using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InventorySystem.UI
{
    public class ItemTooltipUI : MonoBehaviour
    {
        public static ItemTooltipUI Instance { get; private set; }

        [SerializeField] private GameObject _tooltipPanel;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descText;
        private RectTransform _rectTransform;

        private void Awake()
        {
            Instance = this;
            gameObject.SetActive(false);
            _rectTransform = GetComponent<RectTransform>();
        }
        

        public void Show(InventoryItem item)
        {
            _tooltipPanel.SetActive(true);
            _nameText.text = item.itemName;
            _descText.text = item.description;
            Vector2 mousePos = Mouse.current.position.ReadValue();

            Vector2 clampedPos = mousePos;
            clampedPos.x = Mathf.Min(clampedPos.x, Screen.width - _rectTransform.rect.width / 2);
            clampedPos.y = Mathf.Min(clampedPos.y, Screen.height - _rectTransform.rect.height / 2);

            _rectTransform.position = clampedPos;
        }

        public void Hide() => _tooltipPanel.SetActive(false);
    }
}

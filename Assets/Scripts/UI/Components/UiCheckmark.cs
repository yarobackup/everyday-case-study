using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CompanyName.Ui
{
    public class UiCheckmark : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Toggle _toggle;
        [SerializeField] private RectTransform _line;

        public void Init(string text, bool isOn, bool hasLine = true)
        {
            _text.SetText(text);
            _toggle.isOn = isOn;
            _toggle.interactable = false;
            _line.gameObject.SetActive(hasLine);
        }
        public void Subscibe(UnityAction<bool> onToggle)
        {
            _toggle.interactable = true;
            _toggle.onValueChanged.AddListener(onToggle);
        }
        public void Unsubscibe()
        {
            _toggle.interactable = false;
            _toggle.onValueChanged.RemoveAllListeners();
        }
    }
}
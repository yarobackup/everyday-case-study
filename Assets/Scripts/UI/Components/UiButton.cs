using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CompanyName.Ui
{
    public class UiButton : MonoBehaviour
    {
        [SerializeField] private Button _btn;
        [SerializeField] private TMP_Text _text;
        public void Init(string text)
        {
            _text.SetText(text);
        }
        public void Subscibe(UnityAction onClick)
        {
            _btn.onClick.AddListener(onClick);
        }
        public void Unsubscibe()
        {
            _btn.onClick.RemoveAllListeners();
        }
        internal void SetInteractable(bool isInteractable)
        {
            _btn.interactable = isInteractable;
        }
    }
}

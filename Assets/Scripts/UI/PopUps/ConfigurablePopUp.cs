using System;
using System.Collections.Generic;
using CompanyName.Services.SL;
using CompanyName.WindowManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Ui
{
    public class ButtonOptions
    {
        public string Text;
        public Action BtnHandler;

        public bool IsButtonActive => BtnHandler != null;
    }

    public class CloseButtonOptions : ButtonOptions
    {
        public float Delay = 0f;
        public bool IsButtonDelayed => IsButtonActive && Delay > 0;
    }

    public interface IPopUpViewOptions
    {
    }
    public class TextPopUpViewOptions : IPopUpViewOptions
    {
        public string Description { get; }

        public TextPopUpViewOptions(string desc)
        {
            Description = desc;
        }
    }
    public class SpritePopUpViewOptions : IPopUpViewOptions
    {
        public Sprite Sprite;
    }

    public class SettingsPopUpViewOptions : IPopUpViewOptions
    {
        public string Email => "support@example.com";
        public string PrivacyPolicyUrl => "https://example.com/privacy";
        public string RateUsUrl => "https://example.com/rateus";
    }

    public class IPopUpOptions
    {
        public Action OnClosed { get; set; }
    }

    public class ConfigurablePopUpOptions : IPopUpOptions
    {
        public string Title;
        public ConfigurablePopUpType PopUpType;
        public IPopUpViewOptions ViewOptions;
        public ButtonOptions PrimaryButtonOptions;
        public ButtonOptions SecondaryButtonOptions;
        public CloseButtonOptions CloseButtonOptions;
        public Action OnClosed { get; set; }

        public string PopUpName => Screens.PopUp_Configurable;

        public ConfigurablePopUpOptions(string title, ConfigurablePopUpType type, IPopUpViewOptions viewOptions)
        {
            PopUpType = type;
            Title = title;
            ViewOptions = viewOptions;
        }
    }

    public enum ConfigurablePopUpType
    {
        SingleLineDescription,
        TwoLinesDescription,
        SingleSprite,
        Settings
    }

    public class ConfigurablePopUp : UIWindow
    {
        [SerializeField]
        private RectTransform _content;

        [SerializeField]
        private PopUpCloseButton _closeButton;

        [SerializeField]
        private Button _primaryButton;

        [SerializeField]
        private TMP_Text _primaryButtonText;

        [SerializeField]
        private Button _secondaryButton;

        [SerializeField]
        private TMP_Text _secondaryButtonText;

        [SerializeField]
        private TMP_Text _titleText;

        [SerializeField]
        private RectTransform _viewsContainer;

        [SerializeField]
        private List<ConfigurablePopUpView> _views;
        private ConfigurablePopUpView _currentView;

        [Header("Editor")]
        [SerializeField] private string _titleEditor;
        [SerializeField] private string _descriptionEditor;
        [SerializeField] private bool _closeButtonEditor;
        [SerializeField] private bool _primaryButtonEditor;
        [SerializeField] private bool _secondaryButtonEditor;
        [SerializeField] private ConfigurablePopUpType _typeEditor;

        public override string WindowID => Screens.PopUp_Configurable;

        private ConfigurablePopUpOptions _options;

        protected override void OnShowStart()
        {
            base.OnShowStart();
            Context.GetLocalService(out _options);
            ConfigureView(_options);
        }

        protected override void OnShowEnd()
        {
            base.OnShowEnd();
            _closeButton.TryShowCloseButton(_options.CloseButtonOptions);
        }

        public override void Subscribe()
        {
            base.Subscribe();
            TrySubscribeButton(_primaryButton, _options.PrimaryButtonOptions);
            TrySubscribeButton(_secondaryButton, _options.SecondaryButtonOptions);
            _closeButton.TrySubscribe(_options.CloseButtonOptions);
            _currentView.Subscribe();
        }

        public override void Unsubscribe()
        {
            base.Unsubscribe();
            _primaryButton.onClick.RemoveAllListeners();
            _secondaryButton.onClick.RemoveAllListeners();
            _closeButton.Unsubscribe();
            _currentView.Unsubscribe();
        }

        private void ConfigureButton(Button btn, TMP_Text btnText, ButtonOptions btnOptions)
        {
            if (btnOptions == null || !btnOptions.IsButtonActive)
            {
                btn.gameObject.SetActive(false);
                return;
            }
            btn.gameObject.SetActive(true);
            btnText.SetText(btnOptions.Text);
        }

        private void TrySubscribeButton(Button btn, ButtonOptions btnOptions)
        {
            if (btnOptions == null || !btnOptions.IsButtonActive)
            {
                return;
            }
            var btnHandler = btnOptions.BtnHandler;
            btn.onClick.AddListener(() =>
            {
                btnHandler?.Invoke();
            });
        }

        private void ConfigureView(ConfigurablePopUpOptions options)
        {
            for (int i = 0; i < _views.Count; i++)
            {
                if (!_views[i])
                {
                    Debug.LogError("Missing viev object");
                    continue;
                }
                var isCurrentViev = _views[i].ViewType == options.PopUpType;
                if (isCurrentViev)
                {
                    _views[i].ConfigureView(options.ViewOptions);
                    ConfigureSize(_views[i].Height, options);
                    _viewsContainer.sizeDelta = new Vector2(_viewsContainer.sizeDelta.x, _views[i].Height);
                    _currentView = _views[i];
                }
                _views[i].gameObject.SetActive(isCurrentViev);
            }

            _titleText.SetText(options.Title);
            ConfigureButton(_primaryButton, _primaryButtonText, options.PrimaryButtonOptions);
            ConfigureButton(_secondaryButton, _secondaryButtonText, options.SecondaryButtonOptions);
            _closeButton.ConfigureCloseButton(options.CloseButtonOptions);
        }

        private void ConfigureSize(float contentHeight, ConfigurablePopUpOptions options)
        {
            var offsetBig = 16f;
            var offsetSmall = 12f;
            // top offset
            var height = offsetBig;
            // title
            height += _titleText.rectTransform.rect.height;
            // offset title -> content
            height += offsetSmall;
            // Apply offset to content with this height
            _viewsContainer.anchoredPosition = new Vector2(_viewsContainer.anchoredPosition.x, -height);
            // content itselt
            height += contentHeight;
            // offset content -> element

            var hasPrimaryButton = options.PrimaryButtonOptions != null && options.PrimaryButtonOptions.IsButtonActive;
            var hasSecondaryButton = options.SecondaryButtonOptions != null && options.SecondaryButtonOptions.IsButtonActive;
            height += hasPrimaryButton || hasSecondaryButton ? offsetSmall : offsetBig;

            if (hasPrimaryButton)
            {
                var primaryBtnRect = _primaryButton.transform as RectTransform;
                height += primaryBtnRect.rect.height;
                height += hasSecondaryButton ? offsetSmall : offsetBig;
                var yPos = offsetBig;
                if (hasSecondaryButton)
                {
                    yPos += (_secondaryButton.transform as RectTransform).rect.height + offsetSmall;

                }
                primaryBtnRect.anchoredPosition = new Vector2(primaryBtnRect.anchoredPosition.x, yPos);

            }

            if (hasSecondaryButton)
            {
                height += (_secondaryButton.transform as RectTransform).rect.height;
                height += offsetBig;
            }
            _content.sizeDelta = new Vector2(_content.sizeDelta.x, height);
        }

        protected override void OnWindowHideCompleted()
        {
            base.OnWindowHideCompleted();
            _options.OnClosed?.Invoke();
            _options = null;
            _currentView.Reset();
            _currentView = null;
        }

        [Button]
        private void ConfigureForEditor()
        {
            var viewOptions = CreateViewOptions();
            var options = new ConfigurablePopUpOptions(_titleEditor, _typeEditor, viewOptions);
            if (_closeButtonEditor)
            {
                options.CloseButtonOptions = new CloseButtonOptions { Delay = 0f, BtnHandler = () => { } };
            }
            if (_primaryButtonEditor)
            {
                options.PrimaryButtonOptions = new ButtonOptions { Text = "Primary", BtnHandler = () => { } };
            }
            if (_secondaryButtonEditor)
            {
                options.SecondaryButtonOptions = new ButtonOptions { Text = "Secondary", BtnHandler = () => { } };
            }
            ConfigureView(options);
        }

        private IPopUpViewOptions CreateViewOptions()
        {
            switch (_typeEditor)
            {
                case ConfigurablePopUpType.SingleLineDescription:
                case ConfigurablePopUpType.TwoLinesDescription:
                    return new TextPopUpViewOptions(_descriptionEditor);
                case ConfigurablePopUpType.SingleSprite:
                    return new SpritePopUpViewOptions { Sprite = null };
                default:
                    return null;
            }
        }
    }
}

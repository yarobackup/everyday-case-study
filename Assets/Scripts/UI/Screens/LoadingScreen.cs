using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Ui
{
  public class LoadingScreen : MonoBehaviour
  {
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private TMP_Text _sliderText;

    [SerializeField]
    private float _updateSpeed = 5f;

    private float _targetValue;


    private void Update()
    {
      if (!Mathf.Approximately(_slider.value, _targetValue))
      {
        float newValue = Mathf.Lerp(_slider.value, _targetValue, _updateSpeed * Time.deltaTime);
        SetSliderValue(newValue);
      }
    }

    public void OnProgressChanged(float value)
    {
      _targetValue = value;
    }

    public void SetSliderValue(float value)
    {
      _slider.value = value;
      _sliderText.text = $"{Mathf.CeilToInt(value * 100)}%";
    }
  }
}

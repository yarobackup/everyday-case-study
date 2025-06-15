using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CompanyName.Ui
{
  public class GoalPreviewItem : MonoBehaviour
  {
    [SerializeField] private RectTransform _rt;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _count;

    public RectTransform Rt => _rt;

    public void SetData(Sprite image, int count)
    {
      _image.sprite = image;
      _count.text = count.ToString();
    }
  }
}
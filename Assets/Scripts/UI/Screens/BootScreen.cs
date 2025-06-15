using CompanyName.Ui;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

public class BootScreen : ScreenBase<IBootScreenPresenter>
{
  [SerializeField] private Image _logo;
  [SerializeField] private float _initialScale = 1.25f;

  public override string WindowID => Screens.Boot;

  protected override void OnShowStart()
  {
    base.OnShowStart();
    var minWaitTime = _presenter.MinWaitTime;
    var seq = Sequence.Create();
    _logo.transform.localScale = Vector3.one * _initialScale;
    var timings = new float[] { 0.2f, 0.1f, 0.7f };
    seq.Chain(Tween.Alpha(_logo, 0, 1, minWaitTime * timings[0], Ease.OutSine));
    seq.ChainDelay(minWaitTime * timings[1]);
    seq.Chain(Tween.Scale(_logo.transform, _initialScale, 1f, minWaitTime * timings[2], Ease.OutSine));
  }
}

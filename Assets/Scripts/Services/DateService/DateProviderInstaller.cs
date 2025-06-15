using CompanyName.Services.SL;
using UnityEngine;

namespace CompanyName.DateService
{
  public class DateProviderInstaller : GlobalServiceInstaller<IDateProvider, DateProvider>
  {
    [SerializeField] private int _daysToAdd;

    public int DaysToAdd
    {
      get
      {
#if UNITY_EDITOR
        return _daysToAdd;
#else
        return 0;
#endif
      }
    }

    protected override DateProvider CreateService()
    {
      return new DateProvider(DaysToAdd);
    }
  }
}
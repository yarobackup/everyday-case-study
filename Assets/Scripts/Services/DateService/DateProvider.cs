using System;

namespace CompanyName.DateService
{
  public class DateProvider : IDateProvider
  {
    private int _daysToAdd;

    public string Today => DateTime.Today.AddDays(_daysToAdd).ToString("yyyy-MM-dd");

    public DateProvider(int daysToAdd)
    {
      _daysToAdd = daysToAdd;
    }
  }
}
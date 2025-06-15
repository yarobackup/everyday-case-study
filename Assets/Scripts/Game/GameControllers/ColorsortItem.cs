namespace CompanyName.Game.Colorsort
{
  public enum ColorsortItemType
  {
    Type1,
    Type2,
    Type3,
    Type4,
    Type5,
  }

  public class ColorsortItem
  {
    private ColorsortItemType _type;
    private bool _isUnknown;

    public ColorsortItem(ColorsortItemType type, bool isUnknown)
    {
      _type = type;
      _isUnknown = isUnknown;
    }

    public bool IsUnknown
    {
      get => _isUnknown;
      set => _isUnknown = value;
    }
    public ColorsortItemType Type => _type;
  }
}
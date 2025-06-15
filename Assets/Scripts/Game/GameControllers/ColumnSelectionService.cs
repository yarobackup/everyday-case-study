namespace CompanyName.Game.Colorsort
{
  public class ColumnSelectionService
  {
    private int _selectedColumn = -1;
    public int SelectedColumn => _selectedColumn;
    public bool IsColumnSelected => _selectedColumn != -1;

    public void SelectColumn(int column)
    {
      _selectedColumn = column;
    }
  }
}
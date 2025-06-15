namespace CompanyName.ProgressionService
{
  public interface IProgression
  {
    int CurrentStreak { get; }
    CurrentProgress GetCurrent();
    CurrentProgress UpdateCurrent(int index, LevelState state, LevelState? nextLevelState);
  }
}
namespace CompanyName.StorageService
{
  public interface IStorage
  {
    // Generic operations
    void Set<T>(string key, T value);
    T Get<T>(string key, T defaultValue = default(T));
    
    // Utility operations
    bool HasKey(string key);
    void DeleteKey(string key);
    void DeleteAll();
    void Save();
  }
}
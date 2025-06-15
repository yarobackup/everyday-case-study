using UnityEngine;

namespace CompanyName.Extensions
{
  public static class GameObjectUtils
  {
    public static T OrNull<T>(this T obj) where T : Object => obj ? obj : null;
  }
}
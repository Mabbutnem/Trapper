using UnityEngine;

public static class GameObjectUtils
{
   public static GameObject Find(string name)
   {
      GameObject foundObject = GameObject.Find(name);

      if (foundObject == null)
      {
         Debug.LogError(name + " was not found.");
         Debug.Break();
      }

      return foundObject;
   }
}

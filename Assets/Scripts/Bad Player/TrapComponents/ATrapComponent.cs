using UnityEngine;

public abstract class ATrapComponent : MonoBehaviour
{
   private void Start()
   {
      enabled = false;
   }

   public void Execute()
   {
      if (!enabled)
      {
         enabled = true;
         OnExecute();
      }
   }

   public void Stop()
   {
      OnStop();
      enabled = false;
   }

   protected abstract void OnExecute();
   protected virtual void OnStop() { }
}

using UnityEngine;

public abstract class ASpecialTrapController
{
   public abstract void HandleInput();
   public abstract void FixedUpdate();

   public virtual void OnEnter() { }
   public virtual void OnExit() { }
}

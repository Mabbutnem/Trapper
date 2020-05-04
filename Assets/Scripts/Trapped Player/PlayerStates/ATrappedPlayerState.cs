using UnityEngine;

public abstract class ATrappedPlayerState
{
   protected TrappedPlayer trappedPlayer;

   public ATrappedPlayerState(TrappedPlayer trappedPlayer)
   {
      this.trappedPlayer = trappedPlayer;
   }

   public abstract ATrappedPlayerState GetNextState();
   public abstract void HandleInput();
   public abstract void FixedUpdate();

   public virtual void OnEnterState() { }
   public virtual void OnExitState() { }

   public virtual void OnCollisionEnter(Collision collision) { }
   public virtual void OnCollisionExit(Collision collision) { }
}
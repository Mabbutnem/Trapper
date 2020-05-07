using UnityEngine;

public class DeathState : ATrappedPlayerState
{
   private float duration = 0f;

   public DeathState(TrappedPlayer trappedPlayer) : base(trappedPlayer) { }

   public override ATrappedPlayerState GetNextState()
   {
      if (trappedPlayer.MustWait)
      {
         return new WaitState(trappedPlayer);
      }

      if (duration >= trappedPlayer.deathDur)
      {
         return new FlyState(trappedPlayer);
      }

      return this;
   }

   public override void HandleInput()
   {
      //Nothing
   }

   public override void FixedUpdate()
   {
      duration += Time.fixedDeltaTime;
   }

   public override void OnEnterState()
   {
      trappedPlayer.NumberOfLife--;
      trappedPlayer.Rigidbody.constraints = RigidbodyConstraints.None;
   }

   public override void OnExitState()
   {
      trappedPlayer.Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
      if (trappedPlayer.NumberOfLife > 0) { trappedPlayer.Respawn(); }
   }
}
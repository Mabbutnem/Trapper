using UnityEngine;

public class DeathState : ATrappedPlayerState
{
   private float duration = 0f;

   public DeathState(TrappedPlayer trappedPlayer) : base(trappedPlayer) { }

   public override ATrappedPlayerState GetNextState()
   {
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
      trappedPlayer.Rigidbody.constraints = RigidbodyConstraints.None;
   }

   public override void OnExitState()
   {
      trappedPlayer.Rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
      trappedPlayer.Respawn();
   }
}
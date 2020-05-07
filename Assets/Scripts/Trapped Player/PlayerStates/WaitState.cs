using UnityEngine;

public class WaitState : ATrappedPlayerState
{
   public WaitState(TrappedPlayer trappedPlayer) : base(trappedPlayer) { }

   public override ATrappedPlayerState GetNextState()
   {
      if (trappedPlayer.MustWait)
      {
         return this;
      }

      else return new FlyState(trappedPlayer);
   }

   public override void HandleInput()
   {
      //Nothing
   }

   public override void FixedUpdate()
   {
      //Nothing
   }

   public override void OnEnterState()
   {
      trappedPlayer.gameObject.GetComponent<Renderer>().enabled = false;
      trappedPlayer.gameObject.GetComponent<Rigidbody>().isKinematic = true;
      trappedPlayer.gameObject.GetComponent<Collider>().enabled = false;
   }

   public override void OnExitState()
   {
      trappedPlayer.gameObject.GetComponent<Renderer>().enabled = true;
      trappedPlayer.gameObject.GetComponent<Rigidbody>().isKinematic = false;
      trappedPlayer.gameObject.GetComponent<Collider>().enabled = true;
      trappedPlayer.Respawn();
   }
}

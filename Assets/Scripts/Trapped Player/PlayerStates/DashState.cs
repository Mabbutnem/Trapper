using UnityEngine;

public class DashState : ATrappedPlayerState
{
   private float duration = 0f;
   private readonly float direction = 0f;

   public DashState(TrappedPlayer trappedPlayer, float direction) : base(trappedPlayer)
   {
      this.direction = direction;
   }

   public override ATrappedPlayerState GetNextState()
   {
      if (trappedPlayer.MustWait)
      {
         return new WaitState(trappedPlayer);
      }

      if (duration >= trappedPlayer.dashDur)
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
      trappedPlayer.Dash(direction);
   }

   public override void OnEnterState()
   {
      trappedPlayer.CanDie = false;
      trappedPlayer.SetDashColor();
      trappedPlayer.ReloadDash();
   }

   public override void OnExitState()
   {
      trappedPlayer.CanDie = true;
      trappedPlayer.ResetColor();
      trappedPlayer.Rigidbody.velocity = Vector3.zero; //After a Dash, stops quickly.
   }

   public override void OnCollisionEnter(Collision collision)
   {
      if (collision.gameObject.CompareTag(Danger.DANGER_TAG))
      {
         Danger danger = collision.gameObject.GetComponent<Danger>();
         if (danger)
         {
            danger.MakeHarmless(true);
         }
         else
         {
            Debug.LogError("Dangerous object without Danger script ?!");
            Debug.Break();
         }
      }
   }
}

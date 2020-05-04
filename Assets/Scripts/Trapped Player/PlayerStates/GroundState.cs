using UnityEngine;

public class GroundState : ATrappedPlayerState
{
   private float horizontal = 0f;
   private bool jump = false;
   private bool dash = false;

   public GroundState(TrappedPlayer trappedPlayer) : base(trappedPlayer) { }

   public override ATrappedPlayerState GetNextState()
   {
      if(trappedPlayer.MustDie)
      {
         return new DeathState(trappedPlayer);
      }

      if(dash)
      {
         return new DashState(trappedPlayer, horizontal);
      }

      if (trappedPlayer.LeftCheck())
      {
         return new LeftWallState(trappedPlayer);
      }

      if (trappedPlayer.RightCheck())
      {
         return new RightWallState(trappedPlayer);
      }

      if (trappedPlayer.GroundCheck())
      {
         return this;
      }

      return new FlyState(trappedPlayer);
   }

   public override void HandleInput()
   {
      if (Input.GetButtonDown(InputNames.JUMP)) { jump = true; }
      if (Input.GetButtonDown(InputNames.DASH) && trappedPlayer.CanDash) { dash = true; }

      horizontal = Input.GetAxisRaw(InputNames.TRAPPED_HORIZONTAL);
   }

   public override void FixedUpdate()
   {
      trappedPlayer.MoveHorizontal(horizontal);
      if (jump)
      {
         trappedPlayer.Jump();
         jump = false;
      }
   }

   public override void OnEnterState()
   {
      trappedPlayer.ResetJump();

      if (Input.GetButton(InputNames.JUMP)) { jump = true; }
   }
}

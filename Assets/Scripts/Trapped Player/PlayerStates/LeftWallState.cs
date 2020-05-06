using UnityEngine;

public class LeftWallState : ATrappedPlayerState
{
   private static readonly float jumpModificator = Mathf.Sqrt(2);

   private float horizontal = 0f;
   private bool jump = false;
   private bool dash = false;

   public LeftWallState(TrappedPlayer trappedPlayer) : base(trappedPlayer) { }

   public override ATrappedPlayerState GetNextState()
   {
      if (trappedPlayer.MustDie)
      {
         return new DeathState(trappedPlayer);
      }

      if (dash)
      {
         return new DashState(trappedPlayer, 1f);
      }

      if (trappedPlayer.LeftCheck())
      {
         return this;
      }

      return new FlyState(trappedPlayer);
   }

   public override void HandleInput()
   {
      if (Input.GetButtonDown(InputNames.JUMP)) { jump = true; }
      if (Input.GetButtonDown(InputNames.DASH) && trappedPlayer.CanDash) { dash = true; }

      horizontal = Mathf.Max(0f, Input.GetAxisRaw(InputNames.TRAPPED_HORIZONTAL));
   }

   public override void FixedUpdate()
   {
      trappedPlayer.Rigidbody.AddForce(-trappedPlayer.frictionForce * trappedPlayer.Rigidbody.velocity);
      trappedPlayer.MoveHorizontal(horizontal);
      if (jump)
      {
         trappedPlayer.Rigidbody.AddForce(new Vector3(jumpModificator * trappedPlayer.jumpForce, 0f, 0f));
         trappedPlayer.Jump(jumpModificator);
         jump = false;
      }
   }

   public override void OnEnterState()
   {
      Debug.Log("Left Wall");

      trappedPlayer.ResetJump();
   }
}

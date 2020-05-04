using UnityEngine;

public class FlyState : ATrappedPlayerState
{
   private float vertical = 0f;
   private float horizontal = 0f;
   private bool jump = false;
   private bool dash = false;

   public FlyState(TrappedPlayer trappedPlayer) : base(trappedPlayer) { }

   public override ATrappedPlayerState GetNextState()
   {
      if (trappedPlayer.MustDie)
      {
         return new DeathState(trappedPlayer);
      }

      if (dash)
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
         return new GroundState(trappedPlayer);
      }

      return this;
   }

   public override void HandleInput()
   {
      if (Input.GetButtonDown(InputNames.JUMP)) { jump = true; }
      if (Input.GetButtonDown(InputNames.DASH) && trappedPlayer.CanDash) { dash = true; }

      horizontal = Input.GetAxisRaw(InputNames.TRAPPED_HORIZONTAL);
      vertical = Mathf.Min(0f, Input.GetAxisRaw(InputNames.TRAPPED_VERTICAL));
   }

   public override void FixedUpdate()
   {
      trappedPlayer.MoveHorizontal(horizontal);
      trappedPlayer.Rigidbody.velocity += new Vector3(0f, trappedPlayer.goDownVelocity * vertical * Time.fixedDeltaTime, 0f);
      trappedPlayer.Rigidbody.velocity = new Vector3(
         trappedPlayer.Rigidbody.velocity.x,
         Mathf.Max(-trappedPlayer.goDownMaxVelocity, trappedPlayer.Rigidbody.velocity.y),
         trappedPlayer.Rigidbody.velocity.y);

      if (jump)
      {
         trappedPlayer.Jump();
         jump = false;
      }
   }
}

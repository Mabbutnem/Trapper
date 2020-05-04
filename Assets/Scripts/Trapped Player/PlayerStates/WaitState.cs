public class WaitState : ATrappedPlayerState
{
   public WaitState(TrappedPlayer trappedPlayer) : base(trappedPlayer) { }

   public override ATrappedPlayerState GetNextState()
   {
      return this;
   }

   public override void HandleInput()
   {
      //Nothing
   }

   public override void FixedUpdate()
   {
      //Nothing
   }
}

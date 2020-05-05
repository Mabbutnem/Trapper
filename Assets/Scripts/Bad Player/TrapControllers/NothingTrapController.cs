public class NothingTrapController : ATrapController
{
   public NothingTrapController(string name) : base(name) { }

   public override void FixedUpdate()
   {
      //Nothing
   }

   public override void HandleInput()
   {
      //Nothing
   }

   public override void OnEnter()
   {
      //Nothing
   }

   public override void OnExit()
   {
      //Nothing
   }
}

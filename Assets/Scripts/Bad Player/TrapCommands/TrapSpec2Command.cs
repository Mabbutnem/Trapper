public class TrapSpec2Command : ATrapCommand
{
   private BadPlayerController badPlayerController;

   public override void Initialize()
   {
      badPlayerController = GameObjectUtils.Find("Bad Player").GetComponent<BadPlayerController>();
   }

   public override void Execute()
   {
      badPlayerController.SetSpecialTrapController(new GroundSmasherController());
   }
}

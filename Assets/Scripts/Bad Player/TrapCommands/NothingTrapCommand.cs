public class NothingTrapCommand : ATrapCommand
{
   public NothingTrapCommand(string name, float coolDown) : base(name, coolDown) { }

   public override void Execute()
   {
      //Nothing
   }

   public override void StartPreview()
   {
      //Nothing
   }

   public override void StopPreview()
   {
      //Nothing
   }
}

public class SpikesCommand : ATrapCommand
{
   private static readonly int nbSpikes = 10;

   private OpenAndClose[] leftSpikes;
   private OpenAndClose[] rightSpikes;

   public SpikesCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      leftSpikes = new OpenAndClose[nbSpikes];
      rightSpikes = new OpenAndClose[nbSpikes];
      for (int i = 0; i < nbSpikes; i++)
      {
         leftSpikes[i] = GameObjectUtils.Find("Spike Left " + (i+1)).GetComponent<OpenAndClose>();
         rightSpikes[i] = GameObjectUtils.Find("Spike Right " + (i+1)).GetComponent<OpenAndClose>();
      }
      return base.Initialize();
   }

   public override void Execute()
   {
      for (int i = 0; i < nbSpikes; i++)
      {
         leftSpikes[i].OpenWaitClose();
         rightSpikes[i].OpenWaitClose();
      }
   }

   public override void StartPreview()
   {
      for (int i = 0; i < nbSpikes; i++)
      {
         leftSpikes[i].Open();
         rightSpikes[i].Open();
      }
   }

   public override void StopPreview()
   {
      for (int i = 0; i < nbSpikes; i++)
      {
         leftSpikes[i].Close();
         rightSpikes[i].Close();
      }
   }
}

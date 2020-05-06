public class FallingStonesCommand : ATrapCommand
{
   private OpenAndClose fallingStonesLeft;
   private OpenAndClose fallingStonesRight;
   private SpawnStones spawnStones;

   public FallingStonesCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      fallingStonesLeft = GameObjectUtils.Find("Falling Stones Left").GetComponent<OpenAndClose>();
      fallingStonesRight = GameObjectUtils.Find("Falling Stones Right").GetComponent<OpenAndClose>();
      spawnStones = GameObjectUtils.Find("Falling Stones Spawn").GetComponent<SpawnStones>();
      return base.Initialize();
   }

   public override void Execute()
   {
      spawnStones.Spawn();
      fallingStonesLeft.OpenWaitClose();
      fallingStonesRight.OpenWaitClose();
   }

   public override void StartPreview()
   {
      fallingStonesLeft.Open();
      fallingStonesRight.Open();
   }

   public override void StopPreview()
   {
      fallingStonesLeft.Close();
      fallingStonesRight.Close();
   }
}

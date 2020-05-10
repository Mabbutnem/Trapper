public class HoleCommand : ATrapCommand
{
   private static readonly int nbHoles = 6;

   private OpenAndClose[] holes;
   private SolidDisapear realGround;

   public HoleCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      holes = new OpenAndClose[nbHoles];
      for(int i = 0; i < nbHoles; i++)
      {
         holes[i] = GameObjectUtils.Find("Ground " + (i + 1)).GetComponent<OpenAndClose>();
      }
      realGround = GameObjectUtils.Find("Real Ground").GetComponent<SolidDisapear>();

      return base.Initialize();
   }

   public override void Execute()
   {
      foreach(OpenAndClose hole in holes)
      {
         hole.OpenWaitClose();
      }
      realGround.DisapearForSeconds();
   }

   public override void StartPreview()
   {
      foreach (OpenAndClose hole in holes)
      {
         hole.Open();
      }
      realGround.Disapear();
   }

   public override void StopPreview()
   {
      foreach (OpenAndClose hole in holes)
      {
         hole.Close();
      }
      realGround.Apear();
   }
}

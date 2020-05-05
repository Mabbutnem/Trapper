using UnityEngine;

public class TaclerCommand : ATrapCommand
{
   private OpenAndClose leftWall;
   private OpenAndClose rightWall;
   private SpawnSwipeRectangle spawnSwipeRectangle;

   public TaclerCommand(string name, float coolDown) : base(name, coolDown) { }

   public override ATrapCommand Initialize()
   {
      leftWall = GameObjectUtils.Find("Tacler Left Wall").GetComponent<OpenAndClose>();
      rightWall = GameObjectUtils.Find("Tacler Right Wall").GetComponent<OpenAndClose>();
      spawnSwipeRectangle = GameObjectUtils.Find("Tacler Spawn").GetComponent<SpawnSwipeRectangle>();
      return base.Initialize();
   }

   public override void Execute()
   {
      leftWall.OpenWaitClose();
      rightWall.OpenWaitClose();
      spawnSwipeRectangle.Spawn();
   }

   public override void StartPreview()
   {
      leftWall.Open();
      rightWall.Open();
   }

   public override void StopPreview()
   {
      leftWall.Close();
      rightWall.Close();
   }
}

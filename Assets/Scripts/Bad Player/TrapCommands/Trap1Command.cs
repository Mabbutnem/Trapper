public class Trap1Command : ATrapCommand
{
   private DoubleMoveComponent openLeftWall;
   private DoubleMoveComponent openRightWall;
   private SpawnSwipeRectangleComponent spawnSwipeRectangle;

   public override void Initialize()
   {
      openLeftWall = GameObjectUtils.Find("Trap1 Left Wall").GetComponent<DoubleMoveComponent>();
      openRightWall = GameObjectUtils.Find("Trap1 Right Wall").GetComponent<DoubleMoveComponent>();
      spawnSwipeRectangle = GameObjectUtils.Find("Trap1 Spawn").GetComponent<SpawnSwipeRectangleComponent>();
   }

   public override void Execute()
   {
      openLeftWall.Execute();
      openRightWall.Execute();
      spawnSwipeRectangle.Execute();
   }
}

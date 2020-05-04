using UnityEngine;

public class GroundSmasherController : ASpecialTrapController
{
   private int currentSmasher = 2;
   private int nextSmasher = 2;
   private GameObject[] smashers = new GameObject[5];
   private SimpleMoveComponent[] smashersMove = new SimpleMoveComponent[5];
   private Danger[] smashersDanger = new Danger[5];

   public override void OnEnter()
   {
      for(int i = 0; i < smashers.Length; i++)
      {
         smashers[i] = GameObjectUtils.Find("Ground Smasher " + (i+1));
         smashersMove[i] = smashers[i].GetComponent<SimpleMoveComponent>();
         smashersDanger[i] = smashers[i].transform.GetChild(0).gameObject.GetComponent<Danger>();
      }
      Smash(2);
   }

   public override void OnExit()
   {
      Unsmash(currentSmasher);
   }

   public override void HandleInput()
   {
      if (Input.GetKeyDown(KeyCode.LeftArrow)) { DecrSmasher(); }
      if (Input.GetKeyDown(KeyCode.RightArrow)) { IncrSmasher(); }
   }

   public override void FixedUpdate()
   {
      if (nextSmasher != currentSmasher)
      {
         Unsmash(currentSmasher);
         Smash(nextSmasher);
         currentSmasher = nextSmasher;
      }
   }

   private void IncrSmasher()
   {
      if(currentSmasher < 4 &&
         Mathf.Abs(currentSmasher - nextSmasher) == 0
         && !smashersMove[currentSmasher].enabled)
      {
         nextSmasher++;
      }
   }
   private void DecrSmasher()
   {
      if(currentSmasher > 0 &&
         Mathf.Abs(currentSmasher - nextSmasher) == 0
         && !smashersMove[currentSmasher].enabled)
      {
         nextSmasher--;
      }
   }

   public void Smash(int smasherIndex)
   {
      smashersDanger[smasherIndex].MakeDangerousYellow();
      smashersMove[smasherIndex].Execute();
   }

   public void Unsmash(int smasherIndex)
   {
      smashersDanger[smasherIndex].MakeHarmlessBorder();
      smashersMove[smasherIndex].Execute();
   }
}

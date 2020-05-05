using UnityEngine;

public class SmasherController : ATrapController
{
   private int currentSmasher = 2;
   private int nextSmasher = 2;
   private GameObject[] smashers = new GameObject[5];
   private OpenAndClose[] smashersMove = new OpenAndClose[5];
   private Danger[] smashersDanger = new Danger[5];

   public SmasherController(string name) : base(name) { }

   public override void OnEnter()
   {
      for(int i = 0; i < smashers.Length; i++)
      {
         smashers[i] = GameObjectUtils.Find("Smasher " + (i+1));
         smashersMove[i] = smashers[i].GetComponent<OpenAndClose>();
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
         && !smashersMove[currentSmasher].IsExecuting
         && !smashersMove[currentSmasher+1].IsExecuting)
      {
         nextSmasher++;
      }
   }
   private void DecrSmasher()
   {
      if(currentSmasher > 0 &&
         Mathf.Abs(currentSmasher - nextSmasher) == 0
         && !smashersMove[currentSmasher].IsExecuting
         && !smashersMove[currentSmasher-1].IsExecuting)
      {
         nextSmasher--;
      }
   }

   public void Smash(int smasherIndex)
   {
      smashersDanger[smasherIndex].MakeDangerousYellow();
      smashersMove[smasherIndex].Open();
   }

   public void Unsmash(int smasherIndex)
   {
      smashersDanger[smasherIndex].MakeHarmlessBorder();
      smashersMove[smasherIndex].Close();
   }
}

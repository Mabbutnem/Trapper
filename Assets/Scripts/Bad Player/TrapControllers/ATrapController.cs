using UnityEngine;

public abstract class ATrapController
{
   public string Name { get; private set; }

   public ATrapController(string name)
   {
      Name = name;
   }

   public abstract void HandleInput();
   public abstract void FixedUpdate();

   public abstract void OnEnter();
   public abstract void OnExit();
}

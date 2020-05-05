using UnityEngine;

public abstract class ATrapCommand
{
   public string Name { get; private set; }

   public bool IsDone { get; set; } = true;

   public float Duration { get; set; } = 0f;
   public float CoolDown { get; private set; }
   public bool Ready { get; set; } = true;

   public ATrapCommand(string name, float coolDown)
   {
      Name = name;
      CoolDown = coolDown;
   }

   public virtual ATrapCommand Initialize() { return this; }
   public abstract void Execute();
   public abstract void StartPreview();
   public abstract void StopPreview();
}

public class InputTrapCommand
{
   public ATrapCommand TrapCommand { get; set; }
   public string InputName { get; }

   public InputTrapCommand(ATrapCommand trapCommand, string inputName)
   {
      this.TrapCommand = trapCommand;
      this.InputName = inputName;
   }
}
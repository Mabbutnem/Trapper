public abstract class ATrapCommand
{
   public bool IsDone { get; set; } = true;

   public virtual void Initialize() { }
   public abstract void Execute();
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
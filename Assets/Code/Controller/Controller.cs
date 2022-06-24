using System.Collections.Generic;

public class Controller
{
    public const string startMethod = "OnStart";
    public const string updateMethod = "OnUpdate";
    public const string fixedUpdateMethod = "OnFixedUpdate";
    public const string lateUpdate = "OnLateUpdate";
    
    private List<IOnStart> _onStarts = new List<IOnStart>();
    private List<IOnUpdate> _onUpdates = new List<IOnUpdate>();
    
    public Controller Add(IOnController controller)
    {
        if (controller is IOnStart onStart)
        {
            _onStarts.Add(onStart);
        }
            
        if (controller is IOnUpdate onUpdate)
        {
            _onUpdates.Add(onUpdate);
        }
        return this;
    }
    
    public void OnStart()
    {
        foreach (var ell in _onStarts)
        {
            if (ell.HasMethod(startMethod))
            {
                ell.OnStart();
            }
        }
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var ell in _onUpdates)
        {
            if (ell.HasMethod(updateMethod))
            {
                ell.OnUpdate(deltaTime);
            }
        }
    }
}

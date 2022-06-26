using System.Collections.Generic;

public class Controller
{
    private const string StartMethod = "OnStart";
    private const string UpdateMethod = "OnUpdate";

    private readonly List<IOnStart> _onStarts = new List<IOnStart>();
    private readonly List<IOnUpdate> _onUpdates = new List<IOnUpdate>();
    
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
            if (ell.HasMethod(StartMethod))
            {
                ell.OnStart();
            }
        }
    }

    public void OnUpdate(float deltaTime)
    {
        foreach (var ell in _onUpdates)
        {
            if (ell.HasMethod(UpdateMethod))
            {
                ell.OnUpdate(deltaTime);
            }
        }
    }
}

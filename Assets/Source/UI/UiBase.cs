using UnityEngine.EventSystems;

public abstract class UiBase<T> : UIBehaviour where T : UIBehaviour
{
    public T UiComponent
    {
        get
        {
            if (!_uiComponent)
                _uiComponent = GetComponent<T>();
            return _uiComponent;
        }
    }

    private T _uiComponent;

    protected override void OnEnable ()
    {
        base.OnEnable();

        BindUiEvents();
    }

    protected override void OnDisable ()
    {
        base.OnDisable();

        UnbindUiEvents();
    }

    protected abstract void BindUiEvents ();
    protected abstract void UnbindUiEvents ();
}

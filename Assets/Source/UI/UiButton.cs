using UnityEngine.UI;

public abstract class UiButton : UiBase<Button>
{
    protected override void BindUiEvents ()
    {
        UiComponent.onClick.AddListener(OnButtonClick);
    }

    protected override void UnbindUiEvents ()
    {
        UiComponent.onClick.RemoveListener(OnButtonClick);
    }

    protected abstract void OnButtonClick ();
}

using UnityEngine;

public interface ISelectable
{
    public void OnSelect();
    public void OnDeselect();
    public GameObject GetGameObject();
}

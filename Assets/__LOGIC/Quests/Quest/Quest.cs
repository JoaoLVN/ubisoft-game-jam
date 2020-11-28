using UnityEngine;
public abstract class Quest : ScriptableObject
{
    public abstract bool Complete { get; }
    public abstract void Init(ShoppingKart shoppingKart);

}

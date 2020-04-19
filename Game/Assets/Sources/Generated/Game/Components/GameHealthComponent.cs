//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Health health { get { return (Health)GetComponent(GameComponentsLookup.Health); } }
    public bool hasHealth { get { return HasComponent(GameComponentsLookup.Health); } }

    public void AddHealth(int newCurrent, int newMax) {
        var index = GameComponentsLookup.Health;
        var component = (Health)CreateComponent(index, typeof(Health));
        component.current = newCurrent;
        component.max = newMax;
        AddComponent(index, component);
    }

    public void ReplaceHealth(int newCurrent, int newMax) {
        var index = GameComponentsLookup.Health;
        var component = (Health)CreateComponent(index, typeof(Health));
        component.current = newCurrent;
        component.max = newMax;
        ReplaceComponent(index, component);
    }

    public void RemoveHealth() {
        RemoveComponent(GameComponentsLookup.Health);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherHealth;

    public static Entitas.IMatcher<GameEntity> Health {
        get {
            if (_matcherHealth == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Health);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHealth = matcher;
            }

            return _matcherHealth;
        }
    }
}

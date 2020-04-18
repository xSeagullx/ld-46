//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Level level { get { return (Level)GetComponent(GameComponentsLookup.Level); } }
    public bool hasLevel { get { return HasComponent(GameComponentsLookup.Level); } }

    public void AddLevel(int[] newTiles) {
        var index = GameComponentsLookup.Level;
        var component = (Level)CreateComponent(index, typeof(Level));
        component.tiles = newTiles;
        AddComponent(index, component);
    }

    public void ReplaceLevel(int[] newTiles) {
        var index = GameComponentsLookup.Level;
        var component = (Level)CreateComponent(index, typeof(Level));
        component.tiles = newTiles;
        ReplaceComponent(index, component);
    }

    public void RemoveLevel() {
        RemoveComponent(GameComponentsLookup.Level);
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

    static Entitas.IMatcher<GameEntity> _matcherLevel;

    public static Entitas.IMatcher<GameEntity> Level {
        get {
            if (_matcherLevel == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Level);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherLevel = matcher;
            }

            return _matcherLevel;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity roadEntity { get { return GetGroup(GameMatcher.Road).GetSingleEntity(); } }
    public Road road { get { return roadEntity.road; } }
    public bool hasRoad { get { return roadEntity != null; } }

    public GameEntity SetRoad(int newNumLanes, string[] newPattern) {
        if (hasRoad) {
            throw new Entitas.EntitasException("Could not set Road!\n" + this + " already has an entity with Road!",
                "You should check if the context already has a roadEntity before setting it or use context.ReplaceRoad().");
        }
        var entity = CreateEntity();
        entity.AddRoad(newNumLanes, newPattern);
        return entity;
    }

    public void ReplaceRoad(int newNumLanes, string[] newPattern) {
        var entity = roadEntity;
        if (entity == null) {
            entity = SetRoad(newNumLanes, newPattern);
        } else {
            entity.ReplaceRoad(newNumLanes, newPattern);
        }
    }

    public void RemoveRoad() {
        roadEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Road road { get { return (Road)GetComponent(GameComponentsLookup.Road); } }
    public bool hasRoad { get { return HasComponent(GameComponentsLookup.Road); } }

    public void AddRoad(int newNumLanes, string[] newPattern) {
        var index = GameComponentsLookup.Road;
        var component = (Road)CreateComponent(index, typeof(Road));
        component.numLanes = newNumLanes;
        component.pattern = newPattern;
        AddComponent(index, component);
    }

    public void ReplaceRoad(int newNumLanes, string[] newPattern) {
        var index = GameComponentsLookup.Road;
        var component = (Road)CreateComponent(index, typeof(Road));
        component.numLanes = newNumLanes;
        component.pattern = newPattern;
        ReplaceComponent(index, component);
    }

    public void RemoveRoad() {
        RemoveComponent(GameComponentsLookup.Road);
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

    static Entitas.IMatcher<GameEntity> _matcherRoad;

    public static Entitas.IMatcher<GameEntity> Road {
        get {
            if (_matcherRoad == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Road);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRoad = matcher;
            }

            return _matcherRoad;
        }
    }
}

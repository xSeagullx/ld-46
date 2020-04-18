//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RoadPosition roadPosition { get { return (RoadPosition)GetComponent(GameComponentsLookup.RoadPosition); } }
    public bool hasRoadPosition { get { return HasComponent(GameComponentsLookup.RoadPosition); } }

    public void AddRoadPosition(float newDistanceFromStart) {
        var index = GameComponentsLookup.RoadPosition;
        var component = (RoadPosition)CreateComponent(index, typeof(RoadPosition));
        component.distanceFromStart = newDistanceFromStart;
        AddComponent(index, component);
    }

    public void ReplaceRoadPosition(float newDistanceFromStart) {
        var index = GameComponentsLookup.RoadPosition;
        var component = (RoadPosition)CreateComponent(index, typeof(RoadPosition));
        component.distanceFromStart = newDistanceFromStart;
        ReplaceComponent(index, component);
    }

    public void RemoveRoadPosition() {
        RemoveComponent(GameComponentsLookup.RoadPosition);
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

    static Entitas.IMatcher<GameEntity> _matcherRoadPosition;

    public static Entitas.IMatcher<GameEntity> RoadPosition {
        get {
            if (_matcherRoadPosition == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RoadPosition);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRoadPosition = matcher;
            }

            return _matcherRoadPosition;
        }
    }
}

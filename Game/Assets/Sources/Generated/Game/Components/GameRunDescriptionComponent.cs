//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity runDescriptionEntity { get { return GetGroup(GameMatcher.RunDescription).GetSingleEntity(); } }
    public RunDescription runDescription { get { return runDescriptionEntity.runDescription; } }
    public bool hasRunDescription { get { return runDescriptionEntity != null; } }

    public GameEntity SetRunDescription(string newTarget, int newDistanceMeters) {
        if (hasRunDescription) {
            throw new Entitas.EntitasException("Could not set RunDescription!\n" + this + " already has an entity with RunDescription!",
                "You should check if the context already has a runDescriptionEntity before setting it or use context.ReplaceRunDescription().");
        }
        var entity = CreateEntity();
        entity.AddRunDescription(newTarget, newDistanceMeters);
        return entity;
    }

    public void ReplaceRunDescription(string newTarget, int newDistanceMeters) {
        var entity = runDescriptionEntity;
        if (entity == null) {
            entity = SetRunDescription(newTarget, newDistanceMeters);
        } else {
            entity.ReplaceRunDescription(newTarget, newDistanceMeters);
        }
    }

    public void RemoveRunDescription() {
        runDescriptionEntity.Destroy();
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

    public RunDescription runDescription { get { return (RunDescription)GetComponent(GameComponentsLookup.RunDescription); } }
    public bool hasRunDescription { get { return HasComponent(GameComponentsLookup.RunDescription); } }

    public void AddRunDescription(string newTarget, int newDistanceMeters) {
        var index = GameComponentsLookup.RunDescription;
        var component = (RunDescription)CreateComponent(index, typeof(RunDescription));
        component.target = newTarget;
        component.distanceMeters = newDistanceMeters;
        AddComponent(index, component);
    }

    public void ReplaceRunDescription(string newTarget, int newDistanceMeters) {
        var index = GameComponentsLookup.RunDescription;
        var component = (RunDescription)CreateComponent(index, typeof(RunDescription));
        component.target = newTarget;
        component.distanceMeters = newDistanceMeters;
        ReplaceComponent(index, component);
    }

    public void RemoveRunDescription() {
        RemoveComponent(GameComponentsLookup.RunDescription);
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

    static Entitas.IMatcher<GameEntity> _matcherRunDescription;

    public static Entitas.IMatcher<GameEntity> RunDescription {
        get {
            if (_matcherRunDescription == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RunDescription);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRunDescription = matcher;
            }

            return _matcherRunDescription;
        }
    }
}

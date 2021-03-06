//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GlobalContext {

    public GlobalEntity gameStateEntity { get { return GetGroup(GlobalMatcher.GameState).GetSingleEntity(); } }
    public GameState gameState { get { return gameStateEntity.gameState; } }
    public bool hasGameState { get { return gameStateEntity != null; } }

    public GlobalEntity SetGameState(string newState) {
        if (hasGameState) {
            throw new Entitas.EntitasException("Could not set GameState!\n" + this + " already has an entity with GameState!",
                "You should check if the context already has a gameStateEntity before setting it or use context.ReplaceGameState().");
        }
        var entity = CreateEntity();
        entity.AddGameState(newState);
        return entity;
    }

    public void ReplaceGameState(string newState) {
        var entity = gameStateEntity;
        if (entity == null) {
            entity = SetGameState(newState);
        } else {
            entity.ReplaceGameState(newState);
        }
    }

    public void RemoveGameState() {
        gameStateEntity.Destroy();
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
public partial class GlobalEntity {

    public GameState gameState { get { return (GameState)GetComponent(GlobalComponentsLookup.GameState); } }
    public bool hasGameState { get { return HasComponent(GlobalComponentsLookup.GameState); } }

    public void AddGameState(string newState) {
        var index = GlobalComponentsLookup.GameState;
        var component = (GameState)CreateComponent(index, typeof(GameState));
        component.state = newState;
        AddComponent(index, component);
    }

    public void ReplaceGameState(string newState) {
        var index = GlobalComponentsLookup.GameState;
        var component = (GameState)CreateComponent(index, typeof(GameState));
        component.state = newState;
        ReplaceComponent(index, component);
    }

    public void RemoveGameState() {
        RemoveComponent(GlobalComponentsLookup.GameState);
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
public sealed partial class GlobalMatcher {

    static Entitas.IMatcher<GlobalEntity> _matcherGameState;

    public static Entitas.IMatcher<GlobalEntity> GameState {
        get {
            if (_matcherGameState == null) {
                var matcher = (Entitas.Matcher<GlobalEntity>)Entitas.Matcher<GlobalEntity>.AllOf(GlobalComponentsLookup.GameState);
                matcher.componentNames = GlobalComponentsLookup.componentNames;
                _matcherGameState = matcher;
            }

            return _matcherGameState;
        }
    }
}

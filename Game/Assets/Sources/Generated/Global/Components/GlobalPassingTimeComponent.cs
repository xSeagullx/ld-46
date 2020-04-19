//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GlobalEntity {

    public PassingTime passingTime { get { return (PassingTime)GetComponent(GlobalComponentsLookup.PassingTime); } }
    public bool hasPassingTime { get { return HasComponent(GlobalComponentsLookup.PassingTime); } }

    public void AddPassingTime(float newValue) {
        var index = GlobalComponentsLookup.PassingTime;
        var component = (PassingTime)CreateComponent(index, typeof(PassingTime));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplacePassingTime(float newValue) {
        var index = GlobalComponentsLookup.PassingTime;
        var component = (PassingTime)CreateComponent(index, typeof(PassingTime));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemovePassingTime() {
        RemoveComponent(GlobalComponentsLookup.PassingTime);
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

    static Entitas.IMatcher<GlobalEntity> _matcherPassingTime;

    public static Entitas.IMatcher<GlobalEntity> PassingTime {
        get {
            if (_matcherPassingTime == null) {
                var matcher = (Entitas.Matcher<GlobalEntity>)Entitas.Matcher<GlobalEntity>.AllOf(GlobalComponentsLookup.PassingTime);
                matcher.componentNames = GlobalComponentsLookup.componentNames;
                _matcherPassingTime = matcher;
            }

            return _matcherPassingTime;
        }
    }
}

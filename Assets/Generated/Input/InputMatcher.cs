//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ContextMatcherGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class InputMatcher
{
    private Entitas.MatcherInstance<InputEntity> _matcher;
    
    public InputMatcher(Entitas.MatcherInstance<InputEntity> matcher)
    {
        _matcher = matcher;
    }

    public Entitas.IAllOfMatcher<InputEntity> AllOf(params int[] indices) {
        return _matcher.AllOf(indices);
    }

    public Entitas.IAllOfMatcher<InputEntity> AllOf(params Entitas.IMatcher<InputEntity>[] matchers) {
        return _matcher.AllOf(matchers);
    }

    public Entitas.IAnyOfMatcher<InputEntity> AnyOf(params int[] indices) {
        return _matcher.AnyOf(indices);
    }

    public Entitas.IAnyOfMatcher<InputEntity> AnyOf(params Entitas.IMatcher<InputEntity>[] matchers) {
        return _matcher.AnyOf(matchers);
    }
}
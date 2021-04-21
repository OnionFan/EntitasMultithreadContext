namespace Entitas {

    public partial class Matcher<TEntity> : IAllOfMatcher<TEntity> where TEntity : class, IEntity {
        private readonly MatcherInstance<TEntity> _instance;

        public int[] indices {
            get {
                if (_indices == null) {
                    _indices = _instance.mergeIndices(_allOfIndices, _anyOfIndices, _noneOfIndices);
                }
                return _indices;
            }
        }

        public int[] allOfIndices { get { return _allOfIndices; } }
        public int[] anyOfIndices { get { return _anyOfIndices; } }
        public int[] noneOfIndices { get { return _noneOfIndices; } }

        public string[] componentNames { get; set; }

        public int[] _indices;
        public int[] _allOfIndices;
        public int[] _anyOfIndices;
        public int[] _noneOfIndices;

        public Matcher(MatcherInstance<TEntity> instance)
        {
            _instance = instance;
        }

        IAnyOfMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params int[] indices) {
            _anyOfIndices = _instance.distinctIndices(indices);
            _indices = null;
            _isHashCached = false;
            return this;
        }

        IAnyOfMatcher<TEntity> IAllOfMatcher<TEntity>.AnyOf(params IMatcher<TEntity>[] matchers) {
            return ((IAllOfMatcher<TEntity>)this).AnyOf(_instance.mergeIndices(matchers));
        }

        public INoneOfMatcher<TEntity> NoneOf(params int[] indices) {
            _noneOfIndices = _instance.distinctIndices(indices);
            _indices = null;
            _isHashCached = false;
            return this;
        }

        public INoneOfMatcher<TEntity> NoneOf(params IMatcher<TEntity>[] matchers) {
            return NoneOf(_instance.mergeIndices(matchers));
        }

        public bool Matches(TEntity entity) {
            return (_allOfIndices == null || entity.HasComponents(_allOfIndices))
                   && (_anyOfIndices == null || entity.HasAnyComponent(_anyOfIndices))
                   && (_noneOfIndices == null || !entity.HasAnyComponent(_noneOfIndices));
        }
    }
}

using System;
using System.Collections.Generic;

namespace Entitas
{
    public class MatcherInstance<TEntity> where TEntity : class, IEntity
    {
        public readonly List<int> _indexBuffer = new List<int>();
        public readonly HashSet<int> _indexSetBuffer = new HashSet<int>();

        public IAllOfMatcher<TEntity> AllOf(params int[] indices) {
            var matcher = new Matcher<TEntity>(this);
            matcher._allOfIndices = distinctIndices(indices);
            return matcher;
        }

        public IAllOfMatcher<TEntity> AllOf(params IMatcher<TEntity>[] matchers) {
            var allOfMatcher = (Matcher<TEntity>)AllOf(mergeIndices(matchers));
            setComponentNames(allOfMatcher, matchers);
            return allOfMatcher;
        }

        public IAnyOfMatcher<TEntity> AnyOf(params int[] indices) {
            var matcher = new Matcher<TEntity>(this);
            matcher._anyOfIndices = distinctIndices(indices);
            return matcher;
        }

        public IAnyOfMatcher<TEntity> AnyOf(params IMatcher<TEntity>[] matchers) {
            var anyOfMatcher = (Matcher<TEntity>)AnyOf(mergeIndices(matchers));
            setComponentNames(anyOfMatcher, matchers);
            return anyOfMatcher;
        }

        public int[] mergeIndices(int[] allOfIndices, int[] anyOfIndices, int[] noneOfIndices) {
            if (allOfIndices != null) {
                _indexBuffer.AddRange(allOfIndices);
            }
            if (anyOfIndices != null) {
                _indexBuffer.AddRange(anyOfIndices);
            }
            if (noneOfIndices != null) {
                _indexBuffer.AddRange(noneOfIndices);
            }

            var mergedIndices = distinctIndices(_indexBuffer);

            _indexBuffer.Clear();

            return mergedIndices;
        }

        public int[] mergeIndices(IMatcher<TEntity>[] matchers) {
            var indices = new int[matchers.Length];
            for (int i = 0; i < matchers.Length; i++) {
                var matcher = matchers[i];
                if (matcher.indices.Length != 1) {
                    throw new MatcherException(matcher.indices.Length);
                }
                indices[i] = matcher.indices[0];
            }

            return indices;
        }

        public string[] getComponentNames(IMatcher<TEntity>[] matchers) {
            for (int i = 0; i < matchers.Length; i++) {
                var matcher = matchers[i] as Matcher<TEntity>;
                if (matcher != null && matcher.componentNames != null) {
                    return matcher.componentNames;
                }
            }

            return null;
        }

        public void setComponentNames(Matcher<TEntity> matcher, IMatcher<TEntity>[] matchers) {
            var componentNames = getComponentNames(matchers);
            if (componentNames != null) {
                matcher.componentNames = componentNames;
            }
        }

        public int[] distinctIndices(IList<int> indices) {
            foreach (var index in indices) {
                _indexSetBuffer.Add(index);
            }

            var uniqueIndices = new int[_indexSetBuffer.Count];
            _indexSetBuffer.CopyTo(uniqueIndices);
            Array.Sort(uniqueIndices);

            _indexSetBuffer.Clear();

            return uniqueIndices;
        }
    }
}
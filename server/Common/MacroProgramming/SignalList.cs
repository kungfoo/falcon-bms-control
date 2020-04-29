using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.MacroProgramming
{
    public class SignalList<T> : List<T> where T : Signal
    {
        public SignalList()
        {
        }

        public SignalList(int capacity)
            : base(capacity)
        {
        }

        public SignalList(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public T this[string id]
        {
            get { return this.FirstOrDefault(signal => signal.Id == id); }
            set
            {
                if (this[id] == null) return;
                var curVal = this[id];
                Remove(curVal);
                curVal = value;
                Add(curVal);
            }
        }

        public bool Contains(string id)
        {
            return this.Any(signal => signal.Id == id);
        }

        public List<string> GetDistinctSignalCategories()
        {
            var toReturn = new List<string>();
            for (var index = 0; index < Count; index++)
            {
                var signal = this[index];
                if (!string.IsNullOrWhiteSpace(signal.Category) && !toReturn.Contains(signal.Category))
                {
                    toReturn.Add(signal.Category);
                }
            }
            return toReturn;
        }

        public List<string> GetDistinctSignalCollectionNames()
        {
            var toReturn = new List<string>();
            for (var index = 0; index < Count; index++)
            {
                var signal = this[index];
                if (!string.IsNullOrWhiteSpace(signal.CollectionName) && !toReturn.Contains(signal.CollectionName))
                {
                    toReturn.Add(signal.CollectionName);
                }
            }
            return toReturn;
        }

        public List<string> GetDistinctSignalSourceNames()
        {
            var toReturn = new List<string>();

            foreach
            (
                var signal in this
                    .Where(x => !string.IsNullOrWhiteSpace(x.SourceFriendlyName) &&
                                !toReturn.Contains(x.SourceFriendlyName))
            )
                toReturn.Add(signal.SourceFriendlyName);
            return toReturn;
        }

        public SignalList<T> GetSignalsByCategory(string category)
        {
            var toReturn = new SignalList<T>();
            toReturn.AddRange(
                this.Where(
                    signal =>
                        !string.IsNullOrWhiteSpace(signal.Category) &&
                        string.Equals(category, signal.Category, StringComparison.OrdinalIgnoreCase)));
            return toReturn;
        }

        public SignalList<T> GetSignalsByCollection(string collectionName)
        {
            var toReturn = new SignalList<T>();
            toReturn.AddRange(
                this.Where(
                    signal =>
                        !string.IsNullOrWhiteSpace(signal.CollectionName) &&
                        string.Equals(collectionName, signal.CollectionName, StringComparison.OrdinalIgnoreCase)));
            return toReturn;
        }

        public SignalList<T> GetSignalsBySubcollectionName(string collectionName, string subcollectionName)
        {
            var toReturn = new SignalList<T>();
            toReturn.AddRange(
                this.Where(
                    signal =>
                        string.Equals(collectionName, signal.CollectionName, StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(signal.SubcollectionName) &&
                        string.Equals(subcollectionName, signal.SubcollectionName,
                            StringComparison.OrdinalIgnoreCase)));
            return toReturn;
        }

        public SignalList<T> GetSignalsBySubSourceFriendlyName(string subSourceFriendlyName)
        {
            var toReturn = new SignalList<T>();
            toReturn.AddRange(
                this.Where(
                    signal =>
                        !string.IsNullOrWhiteSpace(signal.SubSourceFriendlyName) &&
                        string.Equals(subSourceFriendlyName, signal.SubSourceFriendlyName,
                            StringComparison.OrdinalIgnoreCase)));
            return toReturn;
        }

        public SignalList<T> GetSignalsFromSource(object source)
        {
            var toReturn = new SignalList<T>();
            toReturn.AddRange(
                this.Where(
                    signal =>
                        signal.Source == source ||
                        string.Equals(signal.SourceFriendlyName, source.ToString(),
                            StringComparison.OrdinalIgnoreCase)));
            return toReturn;
        }

        public SignalList<T> GetTopLevelSignals()
        {
            var toReturn = new SignalList<T>();
            toReturn.AddRange(this.Where(signal => signal.SubSource == null));
            return toReturn;
        }

        public List<string> GetUniqueSubcollections(string collectionName)
        {
            var toReturn = new List<string>();

            foreach (var signal in
                this.Where(
                    x =>
                        string.Equals(collectionName, x.CollectionName, StringComparison.OrdinalIgnoreCase) &&
                        !string.IsNullOrWhiteSpace(x.SubcollectionName) &&
                        !toReturn.Contains(x.SubcollectionName)))
                toReturn.Add(signal.SubcollectionName);
            return toReturn;
        }

        public List<string> GetUniqueSubSources()
        {
            var toReturn = new List<string>();
            foreach (var signal in
                this.Where(
                    x =>
                        !string.IsNullOrWhiteSpace(x.SubSourceFriendlyName) &&
                        !toReturn.Contains(x.SubSourceFriendlyName)))
                toReturn.Add(signal.SubSourceFriendlyName);
            return toReturn;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NppArduino.Utils
{
    public class RangeObservableCollection<T> : ObservableCollection<T>
    {
        private bool _suppressNotification;

        public RangeObservableCollection() { }

        public RangeObservableCollection(List<T> list) : base(list) { }

        public RangeObservableCollection(IEnumerable<T> collection) : base(collection) { }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_suppressNotification)
                base.OnCollectionChanged(e);
        }

        public void SuspendNotification()
        {
            _suppressNotification = true;
        }

        public void ResumeNotification()
        {
            _suppressNotification = false;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void SafeRemove(T item)
        {
            if (Contains(item)) Remove(item);
        }

        public void AddRange(IEnumerable<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            CheckReentrancy();
            SuspendNotification();

            foreach (T item in list)
                Items.Add(item);

            ResumeNotification();
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void ReplaceRange(IEnumerable<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            CheckReentrancy();
            SuspendNotification();
            Items.Clear();

            foreach (T item in list)
                Add(item);

            ResumeNotification();
        }

        public void RemoveRange(IEnumerable<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            SuspendNotification();

            foreach (T item in list)
            {
                SafeRemove(item);
            }
            ResumeNotification();
        }

        public void AddUnique(T item)
        {
            if (!Contains(item)) Add(item);
        }
    }
}

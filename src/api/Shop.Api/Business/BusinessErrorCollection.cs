using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Shop.Api.Business
{
    public class BusinessErrorCollection : ICollection<BusinessError>
    {
        private readonly List<BusinessError> _innerList = new List<BusinessError>();

        public void Add(BusinessError item) => _innerList.Add(item);

        public void Add(string code, string message) => _innerList.Add(new BusinessError { Code = code, Message = message });

        public void Clear() => _innerList.Clear();

        public bool Contains(BusinessError item) => _innerList.Contains(item);

        public bool Remove(BusinessError item) => _innerList.Remove(item);

        public int Count => _innerList.Count;

        public bool IsValid => _innerList.Count == 0;

        public bool HasNotFound => _innerList.Any(e => e is BusinessError.NotFound);

        void ICollection<BusinessError>.CopyTo(BusinessError[] array, int arrayIndex) => _innerList.CopyTo(array, arrayIndex);

        IEnumerator<BusinessError> IEnumerable<BusinessError>.GetEnumerator() => _innerList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _innerList.GetEnumerator();

        bool ICollection<BusinessError>.IsReadOnly => false;
    }
}
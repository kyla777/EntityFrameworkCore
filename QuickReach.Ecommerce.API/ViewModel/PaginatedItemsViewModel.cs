using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickReach.Ecommerce.API.ViewModel
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        public int PageIndex { get; private set; }
        public int PageSize { get; private set; }
        public long Count { get; private set; }

        public IEnumerable<TEntity> Data { get; private set; }

        public PaginatedItemsViewModel(int pageIndex, int pageSize, long count, IEnumerable<TEntity> data)
        {
            this.PageIndex = pageIndex; // current page displayed
            this.PageSize = pageSize; // number of items per page
            this.Count = count; 
            this.Data = data;
        }
    }
}

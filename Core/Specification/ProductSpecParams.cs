using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Specification
{
    public class ProductSpecParams
    {
        private const int MaxPageSize=50;//we have only 18 products

        public int PageIndex{get;set;}=1;//By default we are always going to return the very first page

        private int _pageSize=6; //client can choose 1 to millon

        public int PageSize{
            get=>_pageSize;
            set=>_pageSize=(value>MaxPageSize)?MaxPageSize :value ;
        }

        public int?BrandId{get;set;} //int? is the same thing as Nullable. It allows you to have "null" values in your int.the variable can accept a null or an integer or alternatively, return an integer or null.
        public int?TypeId {get;set;}

        public string Sort{get;set;}

        private string _search;

        public  string Search{
            get=>_search;
            set=>_search=value.ToLower();
        }
    }
}
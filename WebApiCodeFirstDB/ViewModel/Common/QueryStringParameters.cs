﻿namespace BlogWebApi.ViewModel.Common
{
    public class QueryStringParameters
    {
        public string? SearchText { get; set; }

        public int MaxPageSize = 50;

        private int _pageSize = 10;

        public int PageNumber { get; set;  } = 1;

        public int PageSize 
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            }

        }

    }
}

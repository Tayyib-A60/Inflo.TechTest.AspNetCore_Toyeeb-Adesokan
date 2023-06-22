using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace UserManagement.Services.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int page, int pageSize)
        {
            if (page <= 0)
                page = 1;

            if (pageSize <= 0)
                pageSize = 10;

            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}


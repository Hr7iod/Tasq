using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryTasqExtensions
    {
        public static IQueryable<Tasq> FilterTasq(this IQueryable<Tasq> tasqs, uint minProgress, uint maxProgress) =>
            tasqs.Where(t => (t.Progress >= minProgress && t.Progress <= maxProgress));

        public static IQueryable<Tasq> SearchName(this IQueryable<Tasq> tasqs, string searchName)
        {
            if (string.IsNullOrWhiteSpace(searchName))
                return tasqs;

            var lowerCaseName = searchName.Trim().ToLower();

            return tasqs.Where(t => t.Name.ToLower().Contains(lowerCaseName));
        }

        public static IQueryable<Tasq> Sort(this IQueryable<Tasq> tasqs, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return tasqs.OrderBy(t => t.Name);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Tasq>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return tasqs.OrderBy(t => t.Name);

            return tasqs.OrderBy(orderQuery);
        }
    }
}

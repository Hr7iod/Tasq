using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}

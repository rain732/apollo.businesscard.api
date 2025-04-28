using AutoMapper;
using AutoMapper.QueryableExtensions;
using Apollo.BusinessCard.Application.Common.Shared;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Apollo.BusinessCard.Application.Common.Mappings;

public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static IQueryable<TSource> Pagination<TSource>(this IQueryable<TSource> source, int pageNumber, int pageSize)
    {
        return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
    public static IQueryable<TSource> WhereIf<TSource>(this IQueryable<TSource> query, bool condition, Expression<Func<TSource, bool>> predicate)
    {
        if (condition == true)
            return query.Where(predicate);
        else
            return query;
    }

}

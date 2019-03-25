using Microsoft.EntityFrameworkCore;
using SpecificationPattern.Demo.CrossCutting.Contracts;
using SpecificationPattern.Demo.CrossCutting.Entities;
using SpecificationPattern.Demo.CrossCutting.Enums;
using SpecificationPattern.Demo.CrossCutting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.Infrastructure.Internal
{
    internal class SpecificationEvaluator<TEntity>
        where TEntity : class, IBaseEntity
    {
        internal static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            IQueryable<TEntity> outputQuery = inputQuery;

            outputQuery = SetCriteria(outputQuery, specification.Criteria);

            outputQuery = SetIncludes(outputQuery, specification.Includes);

            outputQuery = SetPaging(outputQuery, specification.IsPagingEnabled, specification.Skip, specification.Take);

            outputQuery = SetOrderBy(outputQuery, specification.OrderByExpressions);

            return outputQuery;
        }

        private static IQueryable<TEntity> SetPaging(IQueryable<TEntity> outputQuery, bool isPagingEnabled, int skip, int take)
        {
            if (!isPagingEnabled)
            {
                return outputQuery;
            }

            return outputQuery.Skip(skip).Take(take);
        }

        private static IQueryable<TEntity> SetOrderBy(IQueryable<TEntity> outputQuery, IReadOnlyCollection<OrderByExpression<TEntity>> orderByExpressions)
        {
            if (orderByExpressions == null || !orderByExpressions.Any())
            {
                return outputQuery;
            }

            OrderByExpression<TEntity> firstOrderByExpression = orderByExpressions.First();

            IOrderedQueryable<TEntity> orderedQuery = firstOrderByExpression.OrderByDirection.Equals(OrderByDirection.Ascending)
                ? outputQuery.OrderBy(firstOrderByExpression.Expression)
                : outputQuery.OrderByDescending(firstOrderByExpression.Expression);

            foreach (OrderByExpression<TEntity> orderByExpression in orderByExpressions)
            {
                if (orderByExpression.Equals(firstOrderByExpression))
                {
                    continue;
                }

                orderedQuery = orderByExpression.OrderByDirection.Equals(OrderByDirection.Ascending)
                    ? orderedQuery.ThenBy(orderByExpression.Expression)
                    : orderedQuery.ThenByDescending(orderByExpression.Expression);
            }

            return orderedQuery;
        }

        private static IQueryable<TEntity> SetIncludes(IQueryable<TEntity> outputQuery, IReadOnlyCollection<Expression<Func<TEntity, object>>> includes)
        {
            if (includes == null || !includes.Any())
            {
                return outputQuery;
            }

            foreach (Expression<Func<TEntity, object>> argument in includes)
            {
                string include = IncludeExpressionResolver.Resolve(argument);
                outputQuery = outputQuery.Include(include);
            }

            return outputQuery;
        }

        private static IQueryable<TEntity> SetCriteria(IQueryable<TEntity> outputQuery, Expression<Func<TEntity, bool>> criteria)
        {
            if (criteria == null)
            {
                return outputQuery;
            }

            return outputQuery.Where(criteria);
        }
    }
}

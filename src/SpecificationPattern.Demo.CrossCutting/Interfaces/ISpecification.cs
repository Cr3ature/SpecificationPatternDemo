using SpecificationPattern.Demo.CrossCutting.Contracts;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.CrossCutting.Interfaces
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        IReadOnlyCollection<Expression<Func<T, object>>> Includes { get; }

        bool IsPagingEnabled { get; }

        IReadOnlyCollection<OrderByExpression<T>> OrderByExpressions { get; }

        int Skip { get; }

        int Take { get; }
    }
}

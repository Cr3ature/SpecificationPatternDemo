using SpecificationPattern.Demo.CrossCutting.Contracts;
using SpecificationPattern.Demo.CrossCutting.Enums;
using SpecificationPattern.Demo.CrossCutting.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.Host.Specifications
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        private readonly HashSet<Expression<Func<T, object>>> _includes = new HashSet<Expression<Func<T, object>>>();
        private readonly HashSet<OrderByExpression<T>> _orderByExpressions = new HashSet<OrderByExpression<T>>();

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public IReadOnlyCollection<Expression<Func<T, object>>> Includes => _includes;

        public bool IsPagingEnabled { get; private set; } = false;

        public bool IsReadOnly { get; private set; } = false;

        public IReadOnlyCollection<OrderByExpression<T>> OrderByExpressions => _orderByExpressions;

        public int Skip { get; private set; }

        public int Take { get; private set; }

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            _includes.Add(includeExpression);
        }

        protected virtual void AddOrderBy(Expression<Func<T, object>> orderByExpression, OrderByDirection orderByDirection)
        {
            _orderByExpressions.Add(new OrderByExpression<T>(orderByExpression, orderByDirection));
        }

        protected virtual void ApplyPaging(int pageIndex, int pageSize)
        {
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
            IsPagingEnabled = true;
        }

        protected virtual void ApplyReadOnly()
        {
            IsReadOnly = true;
        }
    }
}

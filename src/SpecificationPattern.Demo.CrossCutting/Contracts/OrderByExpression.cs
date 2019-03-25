using SpecificationPattern.Demo.CrossCutting.Enums;
using System;
using System.Linq.Expressions;

namespace SpecificationPattern.Demo.CrossCutting.Contracts
{
    public class OrderByExpression<T>
    {
        public OrderByExpression(Expression<Func<T, object>> expression, OrderByDirection orderByDirection)
        {
            Expression = expression;
            OrderByDirection = orderByDirection;
        }

        public Expression<Func<T, object>> Expression { get; set; }

        public OrderByDirection OrderByDirection { get; set; }
    }
}

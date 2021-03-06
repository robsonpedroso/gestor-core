﻿using Gestor.Tools.Contracts.Repository;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Gestor.Core.Infra.Providers
{
    public class UnitOfWorkFilter : IAsyncActionFilter
    {
        private readonly IUnitOfWorkTransaction transaction;

        public UnitOfWorkFilter(IUnitOfWorkTransaction transaction)
        {
            this.transaction = transaction;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!transaction.IsConnectionOpen)
                throw new NotSupportedException("The provided connection was not open!");

            var executedContext = await next();

            try
            {

                if (executedContext.Exception == null)
                {
                    await transaction.CommitAsync();
                }
                else
                {
                    await transaction.RollbackAsync();
                }
            }
            catch (Exception ex)
            {
                var t = ex;
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Gestor.Tools.Contracts.Repository
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}

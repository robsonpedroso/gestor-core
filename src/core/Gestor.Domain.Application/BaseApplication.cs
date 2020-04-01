using System;
using System.Collections.Generic;
using System.Text;

namespace Gestor.Core.Application
{
    public class BaseApplication : IDisposable
    {
        public void Dispose() => GC.SuppressFinalize(this);
    }
}

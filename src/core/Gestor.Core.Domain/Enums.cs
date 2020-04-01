using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gestor.Core.Domain
{

    public enum TypeAccount
    {
        [Description("Nenhum")]
        None = 0,

        [Description("Funcionário")]
        Employee = 1,

        [Description("Cliente")]
        Client = 2
    }
}

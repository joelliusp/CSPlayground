﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortress
{
    public interface INamingScope
    {
        string GetUniqueName(string suggestedName);
        INamingScope SafeSubScope();
    }
}

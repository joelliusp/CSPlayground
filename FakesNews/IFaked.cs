﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakesNews
{
    public interface IFaked<T>: IFaked
    { }

    public interface IFaked
    {
    }
}
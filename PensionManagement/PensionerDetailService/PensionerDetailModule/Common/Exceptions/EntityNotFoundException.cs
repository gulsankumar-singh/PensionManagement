﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionerDetailModule.Common.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException()
        : base($"Entity was not found.")
    {
        }
    }
}

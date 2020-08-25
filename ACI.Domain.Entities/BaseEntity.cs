using System;
using System.Collections.Generic;
using System.Text;

namespace ACI.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; internal set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Xispirito.Models
{
    public abstract class BaseEntity
    {
        public bool IsActive { get; protected set; }

        public bool GetIsActive()
        {
            return IsActive;
        }

        public void SetIsActive(bool isActive)
        {
            IsActive = isActive;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
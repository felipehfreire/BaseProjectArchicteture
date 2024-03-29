﻿using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CrossCutting.Core.Identity.Models
{
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        bool IsAuthenticated();
        IEnumerable<Claim> GetClaimsIdentity();
    }
}

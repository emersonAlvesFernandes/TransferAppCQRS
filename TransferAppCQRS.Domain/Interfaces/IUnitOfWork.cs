﻿using System;
using TransferAppCQRS.Domain.Core.Commands;

namespace TransferAppCQRS.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        CommandResponse Commit();
    }
}

﻿namespace LETOS.Domain.Abstractions.Entities;
public interface IEntityAuditSoftDelete<T> : IEntityBase<T>, IAuditTable, ISoftDelete
{
}
﻿using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Business.BaseData
{
    public class DataDictionaryDetail : AuditedAggregateRoot<Guid>, ISoftDelete
    {
        public Guid Pid { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public short Sort { get; set; }

        public bool IsDeleted { get; set; }
    }
}

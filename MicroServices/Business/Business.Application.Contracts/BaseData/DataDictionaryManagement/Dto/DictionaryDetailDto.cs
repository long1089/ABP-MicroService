﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Business.BaseData.DataDictionaryManagement.Dto
{
    public class DictionaryDetailDto
    {
        public Guid Pid { get; set; }

        public string Label { get; set; }

        public string Value { get; set; }

        public short Sort { get; set; }
    }
}

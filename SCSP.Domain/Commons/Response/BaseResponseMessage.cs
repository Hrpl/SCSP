﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Commons.Response;

public class BaseResponseMessage
{
    public int StatusCode { get; set; }

    public string? Description { get; set; }
}

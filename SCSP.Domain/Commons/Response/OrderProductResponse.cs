﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCSP.Domain.Commons.Response;

public class OrderProductResponse
{
    public int OrderId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }
    public string Measure { get; set; }
    public double Price { get; set; }
}

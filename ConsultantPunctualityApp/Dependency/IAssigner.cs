﻿using ConsultantPunctualityApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultantPunctualityApp.Dependency
{
    public interface IAssigner
    {
        Task Register(Assigner assigner);
        string GeneratePassword();
    }
}

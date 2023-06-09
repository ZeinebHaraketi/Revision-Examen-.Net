﻿using Examen.ApplicationCore.Domain;
using Examen.ApplicationCore.Interfaces;
using Examen.Interfaces;
using Examen.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examen.ApplicationCore.Services
{
    public class RdvService : Service<RendezVous>, IRdvService
    {
        public RdvService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

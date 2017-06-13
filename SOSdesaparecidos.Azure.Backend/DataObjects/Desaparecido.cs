﻿using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOSdesaparecidos.Azure.Backend.DataObjects
{
    public class Desaparecido : EntityData
    {
        public string IdWeb { get; set; }

        public string Image { get; set; }

        public string UserId { get; set; }
    }
}
﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTWebApp.Application.Exceptions
{
    public class ErrorException: Exception
    {
        public ErrorException()
        {

        }

        public ErrorException(string message)
            : base(message)
        {

        }
        public ErrorException(string message, Exception innerException)
            : base(message, innerException)
        {

        }

        public override string HelpLink
        {
            get
            {
                return "Error occured! Call your Administrator for further information.";
            }
        }
    }
}

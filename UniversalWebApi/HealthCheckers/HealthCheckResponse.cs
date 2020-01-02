using System;
using System.Collections.Generic;

namespace UniversalWebApi.HealthCheckers
{
    public class HealthCheckResponse
    {
        public string Status { get; set; }

        public IEnumerable<HealthCheck> Checks { get; set; }

        public string Duration { get; set; } // Duration that the checks took to complete
    }
}
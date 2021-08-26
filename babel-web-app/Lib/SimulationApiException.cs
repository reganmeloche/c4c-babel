using System;

namespace babel_web_app.Lib
{
    public class SimulationApiException : Exception
    {
        public SimulationApiException()
        {
        }

        public SimulationApiException(string message) : base(message)
        {
        }

        public SimulationApiException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
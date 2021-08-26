using System;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib
{
    public interface IHandleSimulationRequests
    {
         CreateSimulationResponse CreateNewSimulation(CreateSimulationRequest request);
         FullResponse GetSimulationResults(Guid simulationId);
    }
}
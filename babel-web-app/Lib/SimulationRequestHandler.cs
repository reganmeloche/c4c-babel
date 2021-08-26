using System;

using esdc_simulation_classes.MaternityBenefits;

namespace babel_web_app.Lib
{
    public class SimulationRequestHandler : IHandleSimulationRequests
    {
        private readonly string ENDPOINT = "MaternityBenefits";
        private readonly SimulationApi _api;

        public SimulationRequestHandler(SimulationApi api) {
            _api = api;
        }

        public CreateSimulationResponse CreateNewSimulation(CreateSimulationRequest request) {
            var result = _api.ExecutePost<CreateSimulationResponse>(ENDPOINT, request);
            return result;
        }

        public AllSimulationsResponse GetAllSimulations() {
            var result = _api.ExecuteGet<AllSimulationsResponse>(ENDPOINT);
            return result;
        }

        public FullResponse GetSimulationResults(Guid simulationId) {
            var endpoint = $"{ENDPOINT}/{simulationId}/Results";
            var result = _api.ExecuteGet<FullResponse>(endpoint);
            return result;
        }

        public void DeleteSimulation(Guid simulationId) {
            var endpoint = $"{ENDPOINT}/{simulationId}";
            _api.ExecuteDelete(endpoint);
        }
    }
}
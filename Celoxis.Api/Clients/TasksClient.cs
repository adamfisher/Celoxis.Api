using System.Collections.Generic;
using System.Threading.Tasks;
using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients
{
    /// <summary>
    /// Client for task operations
    /// </summary>
    public class TasksClient : EntityClient<CeloxisTask>, ITasksClient
    {
        /// <inheritdoc />
        protected override string EntityPath => "/api/v2/tasks";

        /// <summary>
        /// Initializes a new instance of the TasksClient class
        /// </summary>
        /// <param name="client">Celoxis client instance</param>
        public TasksClient(CeloxisClient client) : base(client) { }

        /// <inheritdoc />
        public async Task<List<CeloxisTask>> GetPredecessorsAsync(int taskId)
        {
            var response = await _client.GetAsync<ApiResponse<List<CeloxisTask>>>($"{EntityPath}/{taskId}/predecessors");
            return response.Data;
        }

        public async Task<List<CeloxisTask>> GetSuccessorsAsync(int taskId)
        {
            var response = await _client.GetAsync<ApiResponse<List<CeloxisTask>>>($"{EntityPath}/{taskId}/successors");
            return response.Data;
        }

        public async Task<List<object>> GetAssignmentsAsync(int taskId)
        {
            var response = await _client.GetAsync<ApiResponse<List<object>>>($"{EntityPath}/{taskId}/assignments");
            return response.Data;
        }
    }
}

using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients
{
    /// <summary>
    /// Client for task update operations
    /// </summary>
    public class TaskUpdatesClient : EntityClient<TaskUpdate>, ITaskUpdatesClient
    {
        /// <inheritdoc />
        protected override string EntityPath => "/api/v2/taskUpdates";

        /// <summary>
        /// Initializes a new instance of the TaskUpdatesClient class
        /// </summary>
        /// <param name="client">Celoxis client instance</param>
        public TaskUpdatesClient(CeloxisClient client) : base(client) { }
    }
}

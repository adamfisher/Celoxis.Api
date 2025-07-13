using Celoxis.Api.Interfaces;
using Celoxis.Api.Models;

namespace Celoxis.Api.Clients;

/// <summary>
/// Client for expense operations
/// </summary>
public class ExpensesClient : EntityClient<CeloxisExpense>, IExpensesClient
{
    /// <inheritdoc />
    protected override string EntityPath => "/api/v2/expenses";

    /// <summary>
    /// Initializes a new instance of the ExpensesClient class
    /// </summary>
    /// <param name="client">Celoxis client instance</param>
    public ExpensesClient(CeloxisClient client) : base(client) { }
}
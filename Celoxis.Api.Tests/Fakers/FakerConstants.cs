namespace Celoxis.Api.Tests.Fakers;

public static class FakerConstants
{
    public static readonly string[] ProjectTypes = { "Infrastructure", "Implementation", "sample", "Celoxis Implementation" };
    
    public static readonly string[] Workspaces = { 
        "DLS", "Templates", "Partnership(s)", "Client(s)", "Samples",
        "New Deals", "Potential Partnership", "Celoxis Sales", "Celoxis Implementations" 
    };
    
    public static readonly string[] HealthStatuses = { "Green", "Yellow", "Red" };
    
    public static readonly string[] ScheduleHealths = { 
        "Not Active", "Off Track", "At Risk", "On Track", "Future", "Completed" 
    };
    
    public static readonly string[] BudgetHealths = { 
        "Not Active", "Off Track", "At Risk", "On Track" 
    };
    
    public static readonly string[] ProjectStates = { 
        "Opportunity", "Draft", "Completed", "Active", "On Hold", "Cancelled", "Test Data" 
    };
    
    public static readonly string[] Priorities = { 
        "NORMAL", "HIGH", "VERY_HIGH" 
    };
    
    public static readonly string[] BillingTypes = { 
        "NONE", "TNM", "FIXED_PRICE" 
    };
    
    public static readonly string[] ScheduleTypes = { 
        "FIXED_WORK", "FIXED_DURATION", "FIXED_UNITS" 
    };
}
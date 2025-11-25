using System;
using System.ServiceModel;

namespace Assignment5_CSE445F25
{
    // This interface defines the WCF contract for the team stats service.
    // [ServiceContract] marks the interface as a WCF service boundary.
    // Each method exposed to clients must be decorated with [OperationContract].
    [ServiceContract]
    public interface ITeamStatsService
    {
        // Retrieves a formatted stats summary string for the specified team.
        // Input: teamName (can be full name, abbreviation, or alias).
        // Output: human-readable stats or a not-found message.
        [OperationContract]
        string GetTeamStats(string teamName);
    }
}

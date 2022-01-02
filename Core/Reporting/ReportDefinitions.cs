using System.IO;
using System.Reflection;

namespace TipTracker.Core.Reporting
{
    /// <summary>
    /// Provides methods to load report definitions into memory streams. 
    /// </summary>
    public static class ReportDefinitions
    {
        /// <summary>
        /// Gets the tip chip report definition stream.
        /// </summary>
        public static Stream TipChit => Assembly.GetAssembly(typeof(ReportDefinitions))
                .GetManifestResourceStream("TipTracker.Core.Reporting.TipChit.rdlc");

        /// <summary>
        /// Gets the payroll totals report definition.
        /// </summary>
        public static Stream PayrollTotals => Assembly.GetAssembly(typeof(ReportDefinitions))
            .GetManifestResourceStream("TipTracker.Core.Reporting.PayrollTotals.rdlc");

        /// <summary>
        /// Gets the event details report definition.
        /// </summary>
        public static Stream EventDetails => Assembly.GetAssembly(typeof(ReportDefinitions))
            .GetManifestResourceStream("TipTracker.Core.Reporting.EventDetail.rdlc");

        /// <summary>
        /// Gets the report definition for tips sorted by server.
        /// </summary>
        public static Stream TipsByServer => Assembly.GetAssembly(typeof(ReportDefinitions))
            .GetManifestResourceStream("TipTracker.Core.Reporting.TipsByServer.rdlc");

        /// <summary>
        /// Gets the report definition for tips sorted by date.
        /// </summary>
        public static Stream TipsByDate => Assembly.GetAssembly(typeof(ReportDefinitions))
            .GetManifestResourceStream("TipTracker.Core.Reporting.TipsByDate.rdlc");
    }
}
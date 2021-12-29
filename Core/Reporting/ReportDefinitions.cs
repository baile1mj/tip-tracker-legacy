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
    }
}
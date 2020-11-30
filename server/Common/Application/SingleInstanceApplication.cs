#region Using statements

using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

#endregion

namespace Common.Application
{
    /// <summary>
    ///     This class ensures that only a single instance of this application is
    ///     ever created.
    /// </summary>
    public sealed class SingleInstanceApplication : WindowsFormsApplicationBase
    {
        /// <summary>
        ///     Constructor that intializes the authentication mode for this app.
        /// </summary>
        /// <param name="mode">Mode in which to run app.</param>
        public SingleInstanceApplication(AuthenticationMode mode)
            : base(mode)
        {
            InitializeAppProperties();
        }

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public SingleInstanceApplication()
        {
            InitializeAppProperties();
        }

        /// <summary>
        ///     Runs the specified mainForm in this application context.
        /// </summary>
        /// <param name="mainForm">Form that is run.</param>
        public void Run(Form mainForm)
        {
            // set up the main form.
            MainForm = mainForm;
            // then, run the the main form.
            Run(CommandLineArgs);
        }


        /// <summary>
        ///     Initializes this application with the appropriate settings.
        /// </summary>
        private void InitializeAppProperties()
        {
            IsSingleInstance = true;
            EnableVisualStyles = false;
        }

        /// <summary>
        ///     Runs this.MainForm in this application context. Converts the command
        ///     line arguments correctly for the base this.Run method.
        /// </summary>
        /// <param name="commandLineArgs">Command line collection.</param>
        private void Run(ReadOnlyCollection<string> commandLineArgs)
        {
            // convert the Collection<string> to string[], so that it can be used
            // in the Run method.
            var list = new ArrayList(commandLineArgs);
            var commandLine = (string[]) list.ToArray(typeof(string));
            Run(commandLine);
        }
    }
}
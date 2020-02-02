using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections;



#region Single Instance

/// <summary>

/// This class ensures that only a single instance of this application is

/// ever created.

/// </summary>


public class SingleInstanceApplication : WindowsFormsApplicationBase
{

    /// <summary>

    /// Constructor that intializes the authentication mode for this app.

    /// </summary>

    /// <param name="mode">Mode in which to run app.</param>

    public SingleInstanceApplication(AuthenticationMode mode)

        : base(mode)
    {

        InitializeAppProperties();

    }



    /// <summary>

    /// Default constructor.

    /// </summary>

    public SingleInstanceApplication()
    {

        InitializeAppProperties();

    }



    /// <summary>

    /// Initializes this application with the appropriate settings.

    /// </summary>

    protected virtual void InitializeAppProperties()
    {

        this.IsSingleInstance = true;

        this.EnableVisualStyles = true;

    }



    /// <summary>

    /// Runs the specified mainForm in this application context.

    /// </summary>

    /// <param name="mainForm">Form that is run.</param>

    public virtual void Run(Form mainForm)
    {

        // set up the main form.

        this.MainForm = mainForm;



        // then, run the the main form.

        this.Run(this.CommandLineArgs);

    }



    /// <summary>

    /// Runs this.MainForm in this application context. Converts the command

    /// line arguments correctly for the base this.Run method.

    /// </summary>

    /// <param name="commandLineArgs">Command line collection.</param>

    private void Run(ReadOnlyCollection<string> commandLineArgs)
    {

        // convert the Collection<string> to string[], so that it can be used

        // in the Run method.

        ArrayList list = new ArrayList(commandLineArgs);

        string[] commandLine = (string[])list.ToArray(typeof(string));

        this.Run(commandLine);

    }

}

#endregion
﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4952
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleHttpServer.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SimpleHttpServer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 1
        ///	(1)
        ///		hostname
        ///		whoami
        ///		who
        ///	(2)
        ///		pwd
        ///		ls -l
        ///	(3)
        ///		date
        ///
        ///2
        ///	if test $# -eq 0
        ///		then
        ///			echo -n &quot;Please input n (integer) : &quot;
        ///			read n
        ///		else
        ///			n=$1
        ///	fi
        ///	if test $# -lt 2
        ///		then
        ///			echo -n &quot;&quot;&gt;temp
        ///			./Hanoi $n &quot;A&quot; &quot;B&quot; &quot;C&quot;
        ///			more temp
        ///			echo &quot;step_n=`wc -l temp`&quot;
        ///			rm temp
        ///		elif test $n -lt 2
        ///			then
        ///				echo &quot;$2 - 1 -&gt; $4&quot;&gt;&gt;temp
        ///		else
        ///			./Hanoi `expr $n - 1` $2 $4 $3
        ///			echo &quot;$2 - $n -&gt; $4&quot;&gt;&gt;temp
        ///			./Hanoi `expr $n - 1` $3 $2 $4
        ///	fi.
        /// </summary>
        internal static string ResponseString {
            get {
                return ResourceManager.GetString("ResponseString", resourceCulture);
            }
        }
    }
}

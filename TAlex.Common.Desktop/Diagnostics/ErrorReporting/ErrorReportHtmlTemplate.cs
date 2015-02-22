﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 12.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace TAlex.Common.Diagnostics.ErrorReporting
{
    using System.Globalization;
    using TAlex.Common.Diagnostics;
    using TAlex.Common.Environment;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public partial class ErrorReportHtmlTemplate : ErrorReportHtmlTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write(@"
<html>
	<head>
		<title>#APPLICATION_ERROR</title>

		<style type=""text/css"">
			html { font-family: Verdana; font-size: 12; line-height: 1.25; color: #606060; }

			h3 { text-align: center; color: #ff0000; }
			h4 { font-size: 12px; color: #51a2f4; font-style: italic; margin-bottom: 0px; }

			table { font-size: 12; line-height: 1.25; margin: auto; border-collapse: collapse; text-align: left; }
			table, table td, table th { border: 1px solid #ff0000; }
			table tr, table td { padding: 4px; margin: 4px; }
			tr.odd-row { background-color: #ffd7d7; }
			tr.even-row { background-color: #febfbf; }
		</style>
	</head>

	<body>
		<h3>Error report summary: ");
            
            #line 25 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(ApplicationInfo.Current.Title + " " + ApplicationInfo.Current.Version));
            
            #line default
            #line hidden
            this.Write("</h2>\r\n\r\n\t\t<p>\r\n\t\t\t<h4>System Information</h4>\r\n\t\t\t<strong>Processor count:</stro" +
                    "ng> ");
            
            #line 29 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Environment.ProcessorCount));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t<strong>Processor architecture:</strong> ");
            
            #line 30 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(SystemInfo.Current.ProcessorArchitecture));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t<strong>Total memory:</strong> ");
            
            #line 31 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(SystemInfo.Current.TotalPhysicalMemoryText));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t<strong>Available memory:</strong> ");
            
            #line 32 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(SystemInfo.Current.AvailablePhysicalMemoryText));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t</p>\r\n\r\n\t\t<p>\r\n\t\t\t<h4>Environment Information</h4>\r\n\t\t\t<strong>Current " +
                    "culture:</strong> ");
            
            #line 37 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(CultureInfo.CurrentCulture));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t<strong>OS:</strong> ");
            
            #line 38 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Environment.OSVersion));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t<strong>Machine name:</strong> ");
            
            #line 39 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Environment.MachineName));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t</p>\r\n\r\n\t\t<p>\r\n\t\t\t<h4>Process Information</h4>\r\n\t\t\t<strong>Command line" +
                    ":</strong> ");
            
            #line 44 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Environment.CommandLine));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t\t<strong>Memory usage:</strong> ");
            
            #line 45 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture((ProcessInfo.Current.PrivateWorkingSet / 1024).ToString("N0") + " K"));
            
            #line default
            #line hidden
            this.Write("<br />\r\n\t\t</p>\r\n\r\n\t\t<table>\r\n\t\t\t<tr class=\"odd-row\">\r\n\t\t\t\t<td width=\"150px\"><stro" +
                    "ng>Error type</strong></td><td>");
            
            #line 50 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.TargetException.GetType()));
            
            #line default
            #line hidden
            this.Write("</td>\r\n\t\t\t</tr>\r\n\t\t\t<tr class=\"even-row\">\r\n\t\t\t\t<td><strong>Error source</strong><" +
                    "/td><td>");
            
            #line 53 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.TargetException.Source));
            
            #line default
            #line hidden
            this.Write("</td>\r\n\t\t\t</tr>\r\n\t\t\t<tr class=\"odd-row\">\r\n\t\t\t\t<td><strong>Target site</strong></t" +
                    "d><td>");
            
            #line 56 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.TargetException.TargetSite));
            
            #line default
            #line hidden
            this.Write("</td>\r\n\t\t\t</tr>\r\n\t\t\t<tr class=\"even-row\">\r\n\t\t\t\t<td><strong>Error message</strong>" +
                    "</td><td>");
            
            #line 59 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.TargetException.Message));
            
            #line default
            #line hidden
            this.Write("</td>\r\n\t\t\t</tr>\r\n\t\t\t<tr class=\"odd-row\">\r\n\t\t\t\t<td><strong>Stack trace</strong></t" +
                    "d><td>");
            
            #line 62 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.TargetException.StackTrace));
            
            #line default
            #line hidden
            this.Write("</td>\r\n\t\t\t</tr>\r\n\t\t\t");
            
            #line 64 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
 if (Model.TargetException.InnerException != null) { 
            
            #line default
            #line hidden
            this.Write("\t\t\t\t<tr class=\"even-row\">\r\n\t\t\t\t\t<td><strong>Inner exception</strong></td><td>");
            
            #line 66 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Model.TargetException.InnerException));
            
            #line default
            #line hidden
            this.Write("</td>\r\n\t\t\t\t</tr>\r\n\t\t\t");
            
            #line 68 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("\t\t</table>\r\n\t</body>\r\n</html>\r\n\r\n");
            return this.GenerationEnvironment.ToString();
        }
        
        #line 73 "D:\Develop\Git\TAlex\Shared\TAlex.Common\trunk\TAlex.Common.Desktop\Diagnostics\ErrorReporting\ErrorReportHtmlTemplate.tt"

	/// <summary>
    /// Gets or sets the model of error report.
	/// </summary>
	public ErrorReport Model { get; set; }

        
        #line default
        #line hidden
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "12.0.0.0")]
    public class ErrorReportHtmlTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
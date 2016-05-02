using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System.Diagnostics;

namespace Xamarin.Forms.Language.Compatibility
{
    public class CodeGenFixupTask : Task
    {
        public string Language
        {
            get; set;
        }

        public override bool Execute()
        {
            var targetAsm = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault((asm) => asm.FullName.StartsWith("Xamarin.Forms.Build.Tasks"));
            var xamlGTaskType = targetAsm.GetType("Xamarin.Forms.Build.Tasks.XamlGTask");
            var providerProperty = xamlGTaskType.GetField("Provider", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            providerProperty.SetValue(null, CodeDomProvider.CreateProvider(Language));
            Trace.WriteLine(providerProperty.GetValue(null));
            return true;
        }
    }
}

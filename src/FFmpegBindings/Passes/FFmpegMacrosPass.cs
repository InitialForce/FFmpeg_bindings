using System;
using System.Collections.Generic;
using System.Linq;
using CppSharp.AST;
using CppSharp.Passes;
using Attribute = CppSharp.AST.Attribute;

namespace FFmpegBindings.Passes
{
    public class FFmpegMacrosPass : TranslationUnitPass
    {
        public override bool VisitDeclaration(Declaration decl)
        {
            if (AlreadyVisited(decl))
                return false;

            var expansions = decl.PreprocessedEntities.OfType<MacroExpansion>();

            if (!expansions.Any())
                return true;

            CheckIgnoreMacros(decl, expansions);
            return true;
        }

        void CheckIgnoreMacros(Declaration decl, IEnumerable<MacroExpansion> expansions)
        {
            if (expansions.Any(e => e.Text.Contains("attribute_deprecated")))
            {
                if (decl.Attributes.All(v => v.Type != typeof (ObsoleteAttribute)))
                {
                    var attribute = new Attribute
                    {
                        Type = typeof (ObsoleteAttribute)
                    };
                    if (decl.DebugText != null)
                    {
                        var commentLines = decl.DebugText.Split('\r','\n');
                        var deprecatedTextLine = commentLines.FirstOrDefault(l => l.Contains("@deprecated"));
                        if(deprecatedTextLine != null)
                        {
                            attribute.Value = "\"" +
                                              deprecatedTextLine.TrimStart('*', ' ').Remove(0, "@deprecated".Length).Trim() +
                                              "\"";
                        }
                    }
                    decl.Attributes.Add(attribute);
                }
            }
        }
    }
}

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
            if (expansions.Any(e => MatchDeclType(decl, e) && e.Text.Contains("attribute_deprecated")))
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

        private bool MatchDeclType(Declaration decl, MacroExpansion macroExpansion)
        {
            if (decl is Class)
                return macroExpansion.Location == MacroLocation.ClassHead;

            if (decl is Function)
                return macroExpansion.Location == MacroLocation.FunctionHead;

            if (decl is Parameter)
                return macroExpansion.Location == MacroLocation.FunctionParameters;

            if (decl is Field)
                return macroExpansion.Location == MacroLocation.ClassBody;

            return false;
        }
    }
}

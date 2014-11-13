using CppSharp.AST;
using CppSharp.Passes;

namespace FFmpegBindings
{
    internal class TypeParameterRenamePass : RenamePass
    {
        private readonly Type _type;
        private readonly string _typeName;

        public TypeParameterRenamePass(string typeName, Type type, RenameTargets targets)
        {
            _typeName = typeName;
            _type = type;
            Targets = targets;
        }

        public override bool Rename(Declaration decl, string name, out string newName)
        {
            if (name == _typeName)
            {
                var p = decl as Parameter;
                if (p != null)
                {
                    p.QualifiedType = new QualifiedType(_type, p.QualifiedType.Qualifiers);
                    newName = _typeName;
                    return true;
                }
            }
            newName = null;
            return false;
        }
    }
}
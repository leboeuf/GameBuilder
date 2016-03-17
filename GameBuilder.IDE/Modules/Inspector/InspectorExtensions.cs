using GameBuilder.IDE.Modules.Inspector.Inspectors.Vector3;
using Gemini.Modules.Inspector;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.IDE.Modules.Inspector
{
    public static class InspectorExtensions
    {
        public static TBuilder WithVector3Editor<TBuilder, T>(this InspectorBuilder<TBuilder> builder,
            T instance, Expression<Func<T, Vector3>> propertyExpression)
            where TBuilder : InspectorBuilder<TBuilder>
        {
            return builder.WithEditor<T, Vector3, Vector3EditorViewModel>(instance, propertyExpression);
        }
    }
}

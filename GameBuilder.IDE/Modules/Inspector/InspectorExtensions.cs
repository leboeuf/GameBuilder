using GameBuilder.IDE.Modules.Inspector.Inspectors.Vector3;
using GameBuilder.IDE.Modules.Inspector.Inspectors.Matrix4;
using Gemini.Modules.Inspector;
using OpenTK;
using System;
using System.Linq.Expressions;

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

        public static TBuilder WithMatrix4Editor<TBuilder, T>(this InspectorBuilder<TBuilder> builder,
            T instance, Expression<Func<T, Matrix4>> propertyExpression)
            where TBuilder : InspectorBuilder<TBuilder>
        {
            return builder.WithEditor<T, Matrix4, Matrix4EditorViewModel>(instance, propertyExpression);
        }
    }
}

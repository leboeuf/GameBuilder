using Gemini.Modules.Inspector.Inspectors;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace GameBuilder.IDE.Modules.Inspector.Inspectors.Vector3
{
    public class Vector3EditorViewModel : EditorBase<OpenTK.Vector3>, ILabelledInspector
    {
        public float X
        {
            get { return Value.X; }
            set
            {
                Value = new OpenTK.Vector3(value, Value.Y, Value.Z);
                NotifyOfPropertyChange(() => X);
            }
        }

        public float Y
        {
            get { return Value.Y; }
            set
            {
                Value = new OpenTK.Vector3(Value.X, value, Value.Z);
                NotifyOfPropertyChange(() => Y);
            }
        }

        public float Z
        {
            get { return Value.Z; }
            set
            {
                Value = new OpenTK.Vector3(Value.X, Value.Y, value);
                NotifyOfPropertyChange(() => Z);
            }
        }

        public override void NotifyOfPropertyChange(string propertyName)
        {
            if (propertyName == "Value")
            {
                NotifyOfPropertyChange(() => X);
                NotifyOfPropertyChange(() => Y);
                NotifyOfPropertyChange(() => Z);
            }
            base.NotifyOfPropertyChange(propertyName);
        }
    }
}

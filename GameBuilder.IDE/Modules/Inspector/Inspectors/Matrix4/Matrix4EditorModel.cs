using Gemini.Modules.Inspector.Inspectors;
using OpenTK;

namespace GameBuilder.IDE.Modules.Inspector.Inspectors.Matrix4
{
    public class Matrix4EditorViewModel : EditorBase<OpenTK.Matrix4>, ILabelledInspector
    {
        public float M11
        {
            get { return Value.M11; }
            set
            {
                Value = new OpenTK.Matrix4(new Vector4(value, Value.M12, Value.M13, Value.M34), Value.Row1, Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M11);
            }
        }

        public float M12
        {
            get { return Value.M12; }
            set
            {
                Value = new OpenTK.Matrix4(new Vector4(Value.M11, value, Value.M13, Value.M34), Value.Row1, Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M12);
            }
        }

        public float M13
        {
            get { return Value.M13; }
            set
            {
                Value = new OpenTK.Matrix4(new Vector4(Value.M11, Value.M12, value, Value.M34), Value.Row1, Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M13);
            }
        }

        public float M14
        {
            get { return Value.M14; }
            set
            {
                Value = new OpenTK.Matrix4(new Vector4(Value.M11, Value.M12, Value.M13, value), Value.Row1, Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M14);
            }
        }

        public float M21
        {
            get { return Value.M21; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, new Vector4(value, Value.M22, Value.M23, Value.M34), Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M21);
            }
        }

        public float M22
        {
            get { return Value.M22; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, new Vector4(Value.M21, value, Value.M23, Value.M34), Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M22);
            }
        }

        public float M23
        {
            get { return Value.M23; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, new Vector4(Value.M21, Value.M22, value, Value.M34), Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M23);
            }
        }

        public float M24
        {
            get { return Value.M24; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, new Vector4(Value.M21, Value.M22, Value.M23, value), Value.Row2, Value.Row3);
                NotifyOfPropertyChange(() => M24);
            }
        }

        public float M31
        {
            get { return Value.M31; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, new Vector4(value, Value.M32, Value.M33, Value.M34), Value.Row3);
                NotifyOfPropertyChange(() => M31);
            }
        }

        public float M32
        {
            get { return Value.M32; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, new Vector4(Value.M31, value, Value.M33, Value.M34), Value.Row3);
                NotifyOfPropertyChange(() => M32);
            }
        }

        public float M33
        {
            get { return Value.M33; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, new Vector4(Value.M31, Value.M32, value, Value.M34), Value.Row3);
                NotifyOfPropertyChange(() => M33);
            }
        }

        public float M34
        {
            get { return Value.M34; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, new Vector4(Value.M31, Value.M32, Value.M33, value), Value.Row3);
                NotifyOfPropertyChange(() => M34);
            }
        }

        public float M41
        {
            get { return Value.M41; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, Value.Row2, new Vector4(value, Value.M42, Value.M43, Value.M44));
                NotifyOfPropertyChange(() => M41);
            }
        }

        public float M42
        {
            get { return Value.M42; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, Value.Row2, new Vector4(Value.M41, value, Value.M43, Value.M44));
                NotifyOfPropertyChange(() => M42);
            }
        }

        public float M43
        {
            get { return Value.M43; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, Value.Row2, new Vector4(Value.M41, Value.M42, value, Value.M44));
                NotifyOfPropertyChange(() => M43);
            }
        }

        public float M44
        {
            get { return Value.M44; }
            set
            {
                Value = new OpenTK.Matrix4(Value.Row0, Value.Row1, Value.Row2, new Vector4(Value.M41, Value.M42, Value.M43, value));
                NotifyOfPropertyChange(() => M44);
            }
        }

        public override void NotifyOfPropertyChange(string propertyName)
        {
            if (propertyName == "Value")
            {
                NotifyOfPropertyChange(() => M11);
                NotifyOfPropertyChange(() => M12);
                NotifyOfPropertyChange(() => M13);
                NotifyOfPropertyChange(() => M14);
                NotifyOfPropertyChange(() => M21);
                NotifyOfPropertyChange(() => M22);
                NotifyOfPropertyChange(() => M23);
                NotifyOfPropertyChange(() => M24);
                NotifyOfPropertyChange(() => M31);
                NotifyOfPropertyChange(() => M32);
                NotifyOfPropertyChange(() => M33);
                NotifyOfPropertyChange(() => M34);
                NotifyOfPropertyChange(() => M41);
                NotifyOfPropertyChange(() => M42);
                NotifyOfPropertyChange(() => M43);
                NotifyOfPropertyChange(() => M44);
            }
            base.NotifyOfPropertyChange(propertyName);
        }
    }
}

using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameBuilder.Library.Cameras
{
    public interface ICamera
    {
        /// <summary>
        /// The coordinates of the camera.
        /// </summary>
        Vector3 Position { get; set; }

        /// <summary>
        /// The coordinates of where the camera is looking at.
        /// </summary>
        Vector3 Target { get; set; }

        /// <summary>
        /// Update the camera's matrixes.
        /// </summary>
        void Update();
    }
}

using System;
using System.Drawing;
using Avalonia;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using Avalonia.Threading;
using Silk.NET.OpenGL;

namespace Tutorial
{
    public class SilkNetExample : OpenGlControlBase
    {
        private GL Gl;
        private BufferObject<float> Vbo;
        private BufferObject<uint> Ebo;
        private VertexArrayObject<float, uint> Vao;
        private Shader Shader;

        private static readonly float[] Vertices =
        {
            //X    Y      Z     R  G  B  A
            0.5f,  0.5f, 0.0f, 1, 0, 0, 1,
            0.5f, -0.5f, 0.0f, 0, 0, 0, 1,
            -0.5f, -0.5f, 0.0f, 0, 0, 1, 1,
            -0.5f,  0.5f, 0.5f, 0, 0, 0, 1
        };

        private static readonly uint[] Indices =
        {
            0, 1, 3,
            1, 2, 3
        };



        protected override void OnOpenGlInit(GlInterface gl, int fb)
        {
            base.OnOpenGlInit(gl, fb);
            
            Gl = GL.GetApi(gl.GetProcAddress);
            

            //Instantiating our new abstractions
            Ebo = new BufferObject<uint>(Gl, Indices, BufferTargetARB.ElementArrayBuffer);
            Vbo = new BufferObject<float>(Gl, Vertices, BufferTargetARB.ArrayBuffer);
            Vao = new VertexArrayObject<float, uint>(Gl, Vbo, Ebo);

            //Telling the VAO object how to lay out the attribute pointers
            Vao.VertexAttributePointer(0, 3, VertexAttribPointerType.Float, 7, 0);
            Vao.VertexAttributePointer(1, 4, VertexAttribPointerType.Float, 7, 3);

            Shader = new Shader(Gl, "shader.vert", "shader.frag");

        }
       

        protected override void OnOpenGlDeinit(GlInterface gl, int fb)
        {
            Vbo.Dispose();
            Ebo.Dispose();
            Vao.Dispose();
            Shader.Dispose();
            base.OnOpenGlDeinit(gl, fb);
        }

        protected override unsafe void OnOpenGlRender(GlInterface gl, int fb)
        {
            Gl.ClearColor(Color.Firebrick);
            Gl.Clear((uint)(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
            Gl.Enable(EnableCap.DepthTest);
            PixelPoint p_tl = this.PointToScreen(Bounds.TopLeft);
            PixelPoint p_br = this.PointToScreen(Bounds.BottomRight);
            PixelPoint p = p_br - p_tl;
            gl.Viewport(0, 0, p.X, p.Y);

            Ebo.Bind();
            Vbo.Bind();
            Vao.Bind();
            Shader.Use();
            Shader.SetUniform("uBlue", (float) Math.Sin(DateTime.Now.Millisecond / 1000f * Math.PI));

            Gl.DrawElements(PrimitiveType.Triangles, (uint) Indices.Length, DrawElementsType.UnsignedInt, null);
            Dispatcher.UIThread.Post(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}
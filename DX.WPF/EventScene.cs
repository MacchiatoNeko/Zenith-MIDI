using System;

namespace DX.WPF
{
    public class EventScene<T> : Scene<T>
        where T : D3D
    {
        public event EventHandler OnAttach;
        public event EventHandler<DrawEventArgs> OnRender;
        public event EventHandler OnDetach;

        protected override void Attach()
        {
            OnAttach?.Invoke(this, new EventArgs());
        }

        protected override void Detach()
        {
            OnDetach?.Invoke(this, new EventArgs());
        }

        public override void RenderScene(DrawEventArgs args)
        {
            OnRender?.Invoke(this, args);
        }
    }
}

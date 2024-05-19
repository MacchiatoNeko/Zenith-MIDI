namespace DX.WPF
{
    public abstract class Scene<R> : IDirect3D
        where R : D3D
    {
        public virtual R Renderer
        {
            get { return context; }
            set
            {
                if (Renderer != null)
                {
                    Renderer.Rendering -= ContextRendering;
                    Detach();
                }
                context = value;
                if (Renderer != null)
                {
                    Renderer.Rendering += ContextRendering;
                    Attach();
                }
            }
        }
        R context;

        void ContextRendering(object aCtx, DrawEventArgs args) { RenderScene(args); }

        protected abstract void Detach();

        protected abstract void Attach();

        public abstract void RenderScene(DrawEventArgs args);

        public void Reset(DrawEventArgs args)
        {
            if (Renderer != null)
                Renderer.Reset(args);
        }

        public void Render(DrawEventArgs args)
        {
            if (Renderer != null)
                Renderer.Render(args);
        }
    }
}

﻿using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenithEngine.DXHelper
{
    public class RasterizerStateKeeper : DeviceInitiable
    {
        public static implicit operator RasterizerState(RasterizerStateKeeper keeper) => keeper.RasterizerState;

        public RasterizerState RasterizerState { get; private set; }
        public RasterizerStateDescription Description;

        public RasterizerStateKeeper CullMode(CullMode mode)
        {
            Description.CullMode = mode;
            return this;
        }

        public static RasterizerStateDescription BasicRasterizerDescription()
        {
            RasterizerStateDescription renderStateDesc = new RasterizerStateDescription
            {
                CullMode = SharpDX.Direct3D11.CullMode.None,
                DepthBias = 0,
                DepthBiasClamp = 0,
                FillMode = FillMode.Solid,
                IsAntialiasedLineEnabled = false,
                IsDepthClipEnabled = false,
                IsFrontCounterClockwise = false,
                IsMultisampleEnabled = true,
                IsScissorEnabled = false,
                SlopeScaledDepthBias = 0
            };

            return renderStateDesc;
        }

        public RasterizerStateKeeper() : this(BasicRasterizerDescription()) { }
        public RasterizerStateKeeper(RasterizerStateDescription desc)
        {
            Description = desc;
        }

        protected override void InitInternal()
        {
            RasterizerState = dispose.Add(new RasterizerState(Device, Description));
        }

        public IDisposable UseOn(DeviceContext ctx) =>
            new Applier<RasterizerState>(this, () => ctx.Rasterizer.State, val => ctx.Rasterizer.State = val);
    }
}

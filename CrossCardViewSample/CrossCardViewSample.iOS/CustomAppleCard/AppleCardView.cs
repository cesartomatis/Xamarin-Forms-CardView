using CoreGraphics;
using System;
using UIKit;

namespace CrossCardViewSample.iOS.CustomAppleCard
{
    public class AppleCardView : UIView
    {
        public CGRect ZeroFrame { get; set; }

        public override CGRect Frame
        {
            get
            {
                return base.Frame;
            }
            set
            {
                base.Frame = value;
                this.DrawBorder(base.Frame, 0.0f);
            }
        }

        public AppleCardView() : base(){}

        public AppleCardView(CGRect frame) : base(frame)
        {
            this.DrawBorder(frame, 0.0f);
        }

        protected void DrawBorder(CGRect rect, nfloat radius)
        {
            this.ZeroFrame = this.Frame;
            this.Layer.MasksToBounds = false;
            this.Layer.ShadowColor = UIColor.Black.CGColor;
            this.Layer.ShadowOffset = new CGSize(0.0f, 0.0f);
            this.Layer.ShadowOpacity = 0.1f;
            this.Layer.BorderColor = UIColor.LightGray.CGColor;
            this.Layer.BorderWidth = 0.1f;
            this.Layer.CornerRadius = radius;
        }

        protected override void Dispose(bool disposing) { }
    }
}

using Android.Support.V7.Widget;
using Android.Views;
using CrossCardViewSample.CustomCardView;
using CrossCardViewSample.Droid.CustomCardRenderer;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomCard), typeof(AndroidCustomCardRenderer))]
namespace CrossCardViewSample.Droid.CustomCardRenderer
{
    public class AndroidCustomCardRenderer : CardView, IVisualElementRenderer
    {
        public AndroidCustomCardRenderer() : base(Forms.Context) { }

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
        ViewGroup packed;
        public void SetElement(VisualElement element)
        {
            var oldElement = this.Element;

            if (oldElement != null)
                oldElement.PropertyChanged -= HandlePropertyChanged;

            this.Element = element;
            if (this.Element != null)
            {

                this.Element.PropertyChanged += HandlePropertyChanged;
            }

            ViewGroup.RemoveAllViews();
            Tracker = new VisualElementTracker(this);

            Packager = new VisualElementPackager(this);
            Packager.Load();

            UseCompatPadding = true;

            SetContentPadding((int)TheView.Padding.Left, (int)TheView.Padding.Top,
                   (int)TheView.Padding.Right, (int)TheView.Padding.Bottom);

            Radius = TheView.CornerRadius;
            SetCardBackgroundColor(TheView.BackgroundColor.ToAndroid());

            if (ElementChanged != null)
                ElementChanged(this, new VisualElementChangedEventArgs(oldElement, this.Element));
        }

        public CustomCard TheView
        {
            get { return this.Element == null ? null : (CustomCard)Element; }
        }

        void HandlePropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Content")
            {
                Tracker.UpdateLayout();
            }
            else if (e.PropertyName == CustomCard.PaddingProperty.PropertyName)
            {
                SetContentPadding((int)TheView.Padding.Left, (int)TheView.Padding.Top,
                    (int)TheView.Padding.Right, (int)TheView.Padding.Bottom);
            }
            else if (e.PropertyName == CustomCard.CornerRadiusProperty.PropertyName)
            {
                this.Radius = TheView.CornerRadius;
            }
            else if (e.PropertyName == CustomCard.BackgroundColorProperty.PropertyName)
            {
                if (TheView.BackgroundColor != null)
                    SetCardBackgroundColor(TheView.BackgroundColor.ToAndroid());
            }
        }

        public SizeRequest GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            packed.Measure(widthConstraint, heightConstraint);
            return new SizeRequest(new Size(packed.MeasuredWidth, packed.MeasuredHeight));
        }

        public void UpdateLayout()
        {
            if (Tracker == null)
                return;

            Tracker.UpdateLayout();
        }

        public VisualElementTracker Tracker { get; private set; }

        public VisualElementPackager Packager { get; private set; }

        public Android.Views.ViewGroup ViewGroup { get { return this; } }

        public VisualElement Element { get; private set; }
    }
}
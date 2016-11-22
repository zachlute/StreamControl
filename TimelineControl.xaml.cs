using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StreamControl {
    /// <summary>
    /// Interaction logic for TimelineControl.xaml
    /// </summary>
    public partial class TimelineControl : UserControl {
        public Revision SelectedRevision
        {
            get;
            private set;
        }

        public TimelineControl () {
            InitializeComponent();
        }

        private void Dot_MouseLeftButtonDown (object sender, MouseButtonEventArgs e) {
            var revision = ((FrameworkElement)sender).DataContext as Revision;
            if (revision != null) {
                if (SelectedRevision != null)
                    SelectedRevision.IsSelected = false;
                revision.IsSelected = true;
                SelectedRevision = revision;
            }
        }

        private void RevisionToolTip_OnOpened (object sender, RoutedEventArgs e) {
            ToolTip toolTip = (ToolTip)sender;
            UIElement target = toolTip.PlacementTarget;
            Point adjust = target.TranslatePoint(new Point(0, 0), toolTip);
            if (adjust.Y > 0) {
                toolTip.Placement = PlacementMode.Top;
            }
            else {
                toolTip.Placement = PlacementMode.Bottom;
            }

            // HACK: Move it up to the source dot.
            if (adjust.Y < -30)
                toolTip.VerticalOffset = 0;

            toolTip.Tag = new Thickness(adjust.X, 1, 0, -1);
        }
    }

    public class TimeAgoConverter : IValueConverter {
        public object Convert (object value, Type targetType, object parameter, CultureInfo culture) {
            return TimeAgo((DateTime)value);
        }

        public object ConvertBack (object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        static string TimeAgo (DateTime dt) {
            TimeSpan span = DateTime.Now - dt;
            if (span.Days > 365) {
                int years = (span.Days / 365);
                if (span.Days % 365 != 0)
                    years += 1;
                return string.Format("{0} {1} ago",
                years, years == 1 ? "year" : "years");
            }
            if (span.Days > 30) {
                int months = (span.Days / 30);
                if (span.Days % 31 != 0)
                    months += 1;
                return string.Format("{0} {1} ago",
                months, months == 1 ? "month" : "months");
            }
            if (span.Days > 0)
                return string.Format("{0} {1} ago",
                span.Days, span.Days == 1 ? "day" : "days");
            if (span.Hours > 0)
                return string.Format("{0} {1} ago",
                span.Hours, span.Hours == 1 ? "hour" : "hours");
            if (span.Minutes > 0)
                return string.Format("{0} {1} ago",
                span.Minutes, span.Minutes == 1 ? "minute" : "minutes");
            if (span.Seconds > 5)
                return string.Format("{0} seconds ago", span.Seconds);
            if (span.Seconds <= 5)
                return "just now";
            return string.Empty;
        }
    }
}

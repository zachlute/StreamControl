using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public class Timeline {
        public IEnumerable<Revision> Revisions
        {
            get;
            set;
        }

        public Timeline () {
            var time = DateTime.Now;
            Revisions = new[] {
                new Revision(time.AddMinutes(-5), "Source created", true, true, false),
                new Revision(time.AddMinutes(-4), "Dest created", false, true, false),
                new Revision(time.AddMinutes(-3), "Change in source", true),
                new Revision(time.AddMinutes(-2), "Change in dest", false), 
                new Revision(time.AddMinutes(-1.5), "Change in source 2", true),
                new Revision(time.AddMinutes(-1), "Change in source 3", true),
                new Revision(time.AddMinutes(-0.5), "Change in dest 2", false),
                new Revision(time.AddMinutes(0), "Change in dest 3", false),
                new Revision(time.AddMinutes(1), "Current", false, isCurrent: true)
            };
        }
    }

    public class Revision : INotifyPropertyChanged {
        public bool IsSource
        {
            get;
        }

        public bool IsDest => !IsSource;

        public bool IsCurrent
        {
            get;
        }

        public bool IsCreate
        {
            get;
        }

        public string Message
        {
            get;
            set;
        }

        public DateTime Time
        {
            get;
        }

        bool m_isSelected;
        public bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                if (m_isSelected != value) {
                    m_isSelected = value;
                    RaisePropertyChanged();
                }
            }
        }

        public Revision(DateTime time, string message, bool isSource, bool isCreate = false, bool isCurrent = false) {
            Time = time;
            Message = message;
            IsSource = isSource;
            IsCreate = isCreate;
            IsCurrent = isCurrent;
        }

        void RaisePropertyChanged([CallerMemberName]string propertyName = "") {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public Timeline Timeline {
            get;
            set;
        } = new Timeline();

        public MainWindow () {
            InitializeComponent();
        }
    }
}
